using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Web.Core.I18N
{
    public class LanguageSettingFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (filterContext.RouteData.Route.GetType().Name == "Route")
            {
                object cultureValueObj;
                if (filterContext.RouteData.Values.TryGetValue("lang", out cultureValueObj))
                {
                    var cultureValue = cultureValueObj.ToString();
                    if (I18N.IsSupport(LanguageTag.Parse(cultureValue)))
                    {
                        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cultureValue.ToString());
                        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureValue.ToString());
                    }
                    else
                    {
                        filterContext.RouteData.Values["lang"] = I18N.BestLanguage(HttpContext.Current.Request.Headers["Accept-Language"]).ToString().ToLower();
                        filterContext.Result = new RedirectToRouteResult(filterContext.RouteData.Values);
                    }
                }
            }
        }
    }
}