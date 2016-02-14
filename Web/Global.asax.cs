using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web.Core.I18N;
using Web.Core;

namespace Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        static LogProvider log = new LogProvider("exc.log");
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            I18N.Initialize();
            I18N.RegisterRoutes(RouteTable.Routes);
            //DirectoryRedirect.Initialize("2015team3");
            //DirectoryRedirect.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

#if DEBUG
        void Application_Error(object sender, EventArgs e)
        {
            //获取到HttpUnhandledException异常，这个异常包含一个实际出现的异常
            Exception ex = Server.GetLastError();
            HttpContext.Current.Response.Write(ex);
            log.LogException(ex);
            Server.ClearError();//处理完及时清理异常
        }
#endif
    }
}