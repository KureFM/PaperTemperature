using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/common").Include("~/scripts/common.js"));

            bundles.Add(new StyleBundle("~/content/css").Include("~/content/common.css"));
        }
    }
}