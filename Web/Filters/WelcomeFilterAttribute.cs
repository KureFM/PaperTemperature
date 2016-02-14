using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Web.Core;
using Web.Core.Extension;
using Web.Core.I18N;

namespace Web.Filters
{
    public class WelcomeFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if ((string)filterContext.RouteData.Values["controller"] != "Language" && !(bool)(HttpContext.Current.Session["Accessed"] ?? false))
            {
                filterContext.Result = new RedirectResult(string.Format("/{0}/welcome", I18N.BestLanguage(HttpContext.Current.Request.Headers["Accept-Language"]).ToString().ToLower()));
            }
        }
    }
}