using System.Web.Mvc;

namespace MvcPL.Infrastructure.Helpers
{
    public static class LoadingHelper
    {
        public static MvcHtmlString CreateElement(this HtmlHelper htmlHelper)
        {
            var allHtml = new TagBuilder("span");

            for (int i = 1; i < 6; i++)
            {
                var divBuilder = new TagBuilder("div");
                var divTwoBuilder = new TagBuilder("div");
                var spanBuilder = new TagBuilder("span");
                var iBuilder = new TagBuilder("i");

                if (i == 5) divBuilder.AddCssClass("cssload-last-finger");
                else divBuilder.AddCssClass("cssload-finger cssload-finger-" + i);

                if (i == 5) divTwoBuilder.AddCssClass("cssload-last-finger-item");
                else divTwoBuilder.AddCssClass("cssload-finger-item");

                divTwoBuilder.InnerHtml += spanBuilder.ToString() + iBuilder.ToString();
                divBuilder.InnerHtml += divTwoBuilder.ToString();

                allHtml.InnerHtml += divBuilder;
            }

            return new MvcHtmlString(allHtml.ToString());
        }
    }
}