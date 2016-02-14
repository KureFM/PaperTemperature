using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;

namespace Web.Core.I18N
{
    public class LanuageConstraint : IRouteConstraint
    {
        private static Regex constraintRegex = new Regex(@"^([a-z]{2,3})(?:\s*?-\s*?([a-z]{4}))?(?:\s*?-\s*?([a-z]{2}))?$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string language = (string)values["lang"];
            if (constraintRegex.IsMatch(language))
            {
                return true;
            }
            return false;
        }
    }
}