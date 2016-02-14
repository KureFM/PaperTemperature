using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Web.Core.Extension;

namespace Web.Core.I18N
{
    public class AcceptLanguage
    {
        protected AcceptLanguage()
        {
        }

        public static List<AcceptLanguageUnit> Parse(string alstr)
        {
            if (alstr.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("值为空，无法解析");
            }

            var alRegex = new Regex(@"([A-Za-z]{2,3})(?:\s*?-\s*?([A-Za-z]{4}))?(?:\s*?-\s*?([A-Za-z]{2}))?(?:;q=([\d.]+))?", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            var matchResult = alRegex.Matches(alstr);

            if (matchResult.Count <= 0)
            {
                throw new ArgumentException("无法解析以下内容: " + alstr);
            }

            // ps: Linq大法好！！！

            var query = from s in
                            from Match m in matchResult
                            select new AcceptLanguageUnit(m.Groups[1].ToString(), m.Groups[2].ToString(), m.Groups[3].ToString(), m.Groups[4].ToString())
                        orderby s.Quality descending
                        select s;


            return query.ToList();
        }
    }

    public class AcceptLanguageUnit : LanguageTag
    {
        public double Quality { get; set; }

        public AcceptLanguageUnit(string language, string script = "", string region = "", double quality = 1)
            : base(language, script, region)
        {
            Quality = quality;
        }

        public AcceptLanguageUnit(string language, string script = "", string region = "", string quality = "")
            : base(language, script, region)
        {
            if (quality.IsNullOrWhiteSpace())
            {
                Quality = 1.0;
            }
            else
            {
                Quality = Convert.ToDouble(quality);
            }
        }
    }
}