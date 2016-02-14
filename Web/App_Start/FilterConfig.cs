using System.Web;
using System.Web.Mvc;
using Web.Core.I18N;
using Web.Filters;

namespace Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new WelcomeFilterAttribute(), 1);
            filters.Add(new LanguageSettingFilterAttribute(), 2);
        }
    }
}