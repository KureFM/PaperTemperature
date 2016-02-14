using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Core.Extension
{
    public static class HtmlHelperExtension
    {
        public static MvcHtmlString NbspQua(this HtmlHelper hh)
        {
            return new MvcHtmlString("&nbsp;&nbsp;&nbsp;&nbsp;");
        }

        public static MvcHtmlString NbspOct(this HtmlHelper hh)
        {
            return new MvcHtmlString("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
        }

        public static MvcHtmlString QuoteWorkLink(this HtmlHelper hh, string url)
        {
            return new MvcHtmlString(string.Format("<a href=\"{0}\">{0}</a>", url));
        }
    }
}