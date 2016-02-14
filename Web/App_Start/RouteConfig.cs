using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //欢迎路由
            routes.MapRoute(
                name: "Welcome",
                url: "welcome",
                defaults: new { controller = "Home", action = "Welcome" }
                );

            //主页路由
            routes.MapRoute(
                name: "Home",
                url: "{home}",
                defaults: new { controller = "Home", action = "Index", home = "Index" },
                constraints: new { home = "(Index|Home|Default|)" }
                );

            //业务路由
            routes.MapRoute(
                name: "Business",
                url: "{controller}/{id}",
                defaults: new { action = "Index" },
                constraints: new { controller = "(Subject|Article)", id = @"\d+" }
                );

            //图片路由
            routes.MapRoute(
                name: "Image",
                url: "Image/{action}/{aid}/{bid}",
                defaults: new { controller = "Image" },
                constraints: new { action = "(Subject|Article)", aid = @"\d+", bid = @"\d+" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}