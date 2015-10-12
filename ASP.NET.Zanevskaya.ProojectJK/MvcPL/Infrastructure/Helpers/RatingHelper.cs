using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcPL.Infrastructure.Helpers
{
    public static class RatingHelper
    {
        public static MvcHtmlString RatingFor<tmodel, TValue >(
    this HtmlHelper<tmodel> htmlHelper, Expression<Func<tmodel, TValue>>
    expression, int from, int to, object htmlAttributes = null)
{
    var builder = new StringBuilder();

    var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

    var model = metadata.Model;
            var name = ExpressionHelper.GetExpressionText(expression);

    var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

    var fullName = 
    htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

    int direction = 1;
    if (from > to)
        direction = -1;

    for (var i = from; direction == 1 ? i <= to : i >= to; i += direction)
    {
        var tagBuilder = new TagBuilder("input");
        tagBuilder.MergeAttributes(attributes);
        tagBuilder.MergeAttribute("type", "radio");
        tagBuilder.MergeAttribute("name", fullName, true);
        tagBuilder.MergeAttribute("value", 
        i.ToString(CultureInfo.InvariantCulture));
        //If model has a value we need to select it
        if (model != null && model.Equals(i))
        {
            tagBuilder.MergeAttribute("checked", "checked");
        }
        tagBuilder.GenerateId(fullName);

        ModelState modelState;
        if (htmlHelper.ViewData.ModelState.TryGetValue(fullName, out modelState))
        {
            if (modelState.Errors.Count > 0)
            {
                tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
            }
        }
        tagBuilder.MergeAttributes(htmlHelper.
        GetUnobtrusiveValidationAttributes(name, metadata));

        builder.AppendLine(tagBuilder.ToString(TagRenderMode.SelfClosing));
    }

    return MvcHtmlString.Create(builder.ToString());
}
    }
}