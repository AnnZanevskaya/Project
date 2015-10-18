using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPL.Infrastructure.Helpers
{
    public class ReferrerPageNameAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var referrer = filterContext.RequestContext.HttpContext.Request.UrlReferrer;
            if (referrer != null)
            {
                filterContext.RouteData.Values.Add("referrer", referrer);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}