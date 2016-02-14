using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Core
{
    public static class DirectoryRedirect
    {
        public static string DirName { private set; get; }

        public static void Initialize(string dirName)
        {
            DirName = dirName;
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            if (routes == null)
            {
                return;
            }
            var rs = from r in routes
                     where r.GetType().Name == "Route"
                     select r;

            foreach (Route r in rs)
            {
                r.Url = "{" + DirName + "}/" + r.Url;
                // r.Constraints.Add("dir", new DirectoryConstraint(DirName));
            }

            ////目录重定向路由
            //routes.MapRoute(
            //    "Dir",
            //    "{*routePath}",
            //    new { controller = "Language", action = "Index" }
            //    );
        }
    }

    //// 目录重定向控制器
    //public class DirectoryController : Controller
    //{
    //    // GET: /Directory/
    //    public ActionResult Index(string routePath)
    //    {
    //        return Redirect(String.Format("/{0}/{1}", DirectoryRedirect.DirName, routePath));
    //    }
    //}

    //public class DirectoryConstraint : IRouteConstraint
    //{
    //    public static string DirName { private set; get; }

    //    public DirectoryConstraint(string dirName)
    //    {
    //        DirName = dirName;
    //    }
    //    public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
    //    {
    //        string dirName = ((string)values["dir"]).ToLower();
    //        if (dirName == DirName)
    //        {
    //            return true;
    //        }
    //        return false;
    //    }
    //}
}