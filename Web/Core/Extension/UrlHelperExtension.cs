using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Web.Core.I18N;

namespace Web.Core.Extension
{
    public static class UrlHelperExtension
    {
        public static string IndexUrl(this UrlHelper uh)
        {
            return uh.RouteUrl("Home", new { home = "index" });
        }

        public static string SubjectUrl(this UrlHelper uh, int id)
        {
            return uh.RouteUrl("Business", new { controller = "subject", id = string.Format("{0:D2}", id) });
        }

        public static string ArticleUrl(this UrlHelper uh, int id)
        {
            return uh.RouteUrl("Business", new { controller = "article", id = string.Format("{0:D2}", id) });
        }

        public static string ImageUrl(this UrlHelper uh, ImageTo it, int aid, int bid)
        {
            return uh.RouteUrl("Image", new { action = it.ToString(), aid = aid, bid = bid });
        }

        public static string ChangeLanguage(this UrlHelper uh, RouteValueDictionary rvd, string language)
        {
            var tempRVD = new RouteValueDictionary(rvd);
            tempRVD["lang"] = language;
            return uh.RouteUrl(tempRVD);
        }

        public static string ChangeLanguage(this UrlHelper uh, RouteValueDictionary rvd, LanguageTag lt)
        {
            return uh.ChangeLanguage(rvd, lt.ToString().ToLower());
        }
    }

    [Flags]
    public enum ImageTo
    {
        Subject,
        Article
    }
}