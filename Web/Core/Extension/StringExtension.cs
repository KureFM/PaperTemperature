using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace Web.Core.Extension
{
    public static class StringExtension
    {
        #region Format
        public static string Format(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static string Format(this string format, IFormatProvider provider, params object[] args)
        {
            return string.Format(provider, format, args);
        }

        public static string Format(this string format, object arg0)
        {
            return string.Format(format, arg0);
        }

        public static string Format(this string format, object arg0, object arg1)
        {
            return string.Format(format, arg0, arg1);
        }

        public static string Format(this string format, object arg0, object arg1, object arg2)
        {
            return string.Format(format, arg0, arg1, arg2);
        }
        #endregion

        #region Judge
        /// <summary>
        /// 指示指定的字符串是 null 还是 System.String.Empty 字符串。
        /// </summary>
        /// <param name="value">要测试的字符串。</param>
        /// <returns>如果 value 参数为 null 或空字符串 ("")，则为 true；否则为 false。</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 指示指定的字符串是 null、空还是仅由空白字符组成。
        /// </summary>
        /// <param name="value">要测试的字符串。</param>
        /// <returns>如果 value 参数为 null 或 System.String.Empty，或者如果 value 仅由空白字符组成，则为 true。</returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        #endregion

        #region ToTitleCase
        public static string ToTitleCase(this string str, CultureInfo ci = null)
        {
            if (ci == null)
            {
                ci = CultureInfo.InvariantCulture;
            }
            return ci.TextInfo.ToTitleCase(str);
        }
        #endregion
    }
}