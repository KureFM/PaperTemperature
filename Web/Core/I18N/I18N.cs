using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Web.Core.Extension;

namespace Web.Core.I18N
{
    /// <summary>
    /// 为应用程序提供语言国际化的支持
    /// </summary>
    public class I18N
    {
        #region 构造函数和变量

        protected static Dictionary<LanguageTag, List<LanguageTag>> supportLanguage;

        protected static LanguageTag defaultLanguage;


        protected I18N()
        {

        }

        public static Dictionary<LanguageTag, List<LanguageTag>> SupportLanguage
        {
            get
            {
                return supportLanguage;
            }
        }

        public static LanguageTag DefaultLanguage
        {
            get
            {
                return defaultLanguage;
            }
        }

        #endregion

        #region 初始化，配置支持语言

        /// <summary>
        /// 初始化I18N支持环境
        /// </summary>
        public static void Initialize()
        {
            if (supportLanguage != null)
            {
                return;
            }

            supportLanguage = new Dictionary<LanguageTag, List<LanguageTag>>();

            SupportedLanguageSection sl = (SupportedLanguageSection)ConfigurationManager.GetSection("supportedLanguage");

            foreach (LanguageElement item in sl.Language)
            {
                List<LanguageTag> fallbackList;

                if (item.Fallback)
                {
                    fallbackList = FallbackLanguage(item.Name);
                }
                else
                {
                    fallbackList = new List<LanguageTag>()
                    {
                        LanguageTag.Parse(item.Name)
                    };
                }

                supportLanguage[LanguageTag.Parse(item.Name)] = fallbackList;

                defaultLanguage = LanguageTag.Parse(sl.DefaultLanguage);
            }
        }

        protected static List<LanguageTag> FallbackLanguage(string name)
        {
            var ci = new CultureInfo(name);
            var fallbackList = new List<LanguageTag>();
            while (!ci.Name.IsNullOrWhiteSpace())
            {
                // .Net中有部分语言不符合RFC 4646规范，直接抛弃
                try
                {
                    fallbackList.Add(LanguageTag.Parse(ci.Name));
                }
                catch (ArgumentException)
                {
                }
                ci = ci.Parent;
            }
            return fallbackList;
        }

        #endregion

        #region 配置路由表以支持I18N环境
        /// <summary>
        /// 为所在的路由集合添加语言参数，并添加语言重定向路由
        /// </summary>
        /// <param name="routes"></param>
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
                r.Url = "{lang}/" + r.Url;
                r.Constraints.Add("lang", new LanuageConstraint());
            }

            //语言重定向路由
            routes.MapRoute(
                "Language",
                "{*routePath}",
                new { controller = "Language", action = "Index" }
                );
        }

        #endregion

        #region 匹配最合适的语言
        /// <summary>
        /// 从支持语言中选取最合适的语言，其中alList是按优先级排列的接受语言（Accep-Langage）集合
        /// </summary>
        /// <param name="alList">接受语言（Accep-Langage）集合</param>
        /// <returns></returns>
        public static LanguageTag BestLanguage(IEnumerable<LanguageTag> alList)
        {
            var bestLanguageList = new Dictionary<LanguageTag, int>();
            foreach (var al in alList)
            {
                var bestSLList = new Dictionary<LanguageTag, int>();
                foreach (var sl in I18N.supportLanguage.Keys)
                {
                    int maxWeight = 0;
                    foreach (var item in I18N.supportLanguage[sl])
                    {
                        maxWeight = 0;
                        //如果找到完全相等的语言标签，直接返回
                        if (al == item)
                        {
                            return sl;
                        }
                        // 不完全相等的语言标签，计算当前SupportedLanguage的最大权值
                        else
                        {
                            int weight = item.Match(al);
                            weight = weight > maxWeight ? weight : maxWeight;
                        }
                    }
                    // 只有权值不为0的时候才认为当前语言标签有可能被匹配，将其和权值一同加入最优支持语言列表（bestSLList）
                    if (maxWeight > 0)
                    {
                        bestSLList.Add(sl, maxWeight);
                    }
                }
                // 若最优支持语言列表（bestSLList）不为空，取权值最大的语言标签加入最优语言列表（bestLanguageList）
                if (bestSLList.Count > 0)
                {
                    var bestSL = bestSLList.OrderByDescending(x => x.Value).ToList()[0];
                    bestLanguageList.Add(bestSL.Key, bestSL.Value);
                }
            }
            // 若最优语言列表（bestLanguageList）不为空，则其权值最大的语言标签为最优语言标签
            if (bestLanguageList.Count > 0)
            {
                return bestLanguageList.OrderByDescending(x => x.Value).ToList()[0].Key;
            }


            return I18N.defaultLanguage;
        }

        /// <summary>
        /// 解析接受语言（Accep-Langage）字符串，从支持语言中选取最合适的语言
        /// </summary>
        /// <param name="alString">解析接受语言（Accep-Langage）字符串</param>
        /// <returns></returns>
        public static LanguageTag BestLanguage(string alString)
        {
            if (alString.IsNullOrWhiteSpace())
            {
                return DefaultLanguage;
            }
            return BestLanguage(AcceptLanguage.Parse(alString));
        }

        #endregion

        #region 检查语言是否被支持
        public static bool IsSupport(LanguageTag lt)
        {
            foreach (var sl in SupportLanguage.Keys)
            {
                if (lt == sl)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsSupport(string language)
        {
            if (language.IsNullOrWhiteSpace())
            {
                return IsSupport(LanguageTag.Parse(language));
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 为url补充语言参数
        public static string AddLanguage(string language, string url)
        {
            return string.Format("/{0}/{1}", language, url);
        }

        public static string AddLanguage(LanguageTag lt, string url)
        {
            return AddLanguage(lt.ToString().ToLower(), url);
        }
        public static string AddLanguage(CultureInfo ci, string url)
        {
            return AddLanguage(ci.Name.ToLower(), url);
        }

        public static string AutoLanguage(string url)
        {
            return AddLanguage(Thread.CurrentThread.CurrentUICulture, url);
        }
        #endregion

        #region 查看当前语言字符串

        public static string CurrentLanguage
        {
            get
            {
                return Thread.CurrentThread.CurrentUICulture.ToString().ToLower();
            }
        }

        #endregion
    }

    // 语言重定向控制器
    public class LanguageController : Controller
    {
        // GET: /Language/
        public ActionResult Index(string routePath)
        {
            string language = I18N.BestLanguage(Request.Headers["Accept-Language"]).ToString().ToLower();
            return Redirect(String.Format("/{0}/{1}", language, routePath));
        }
    }
}