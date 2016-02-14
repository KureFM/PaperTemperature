using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Web.Core.Extension;

namespace Web.Core.I18N
{
    /// <summary>
    /// 依据RFC 4646, 当前的语言参数共分为language, script, region. 其中language为必须, script, region为可选.
    /// </summary>
    public class LanguageTag
    {
        protected string language;
        protected string script;
        protected string region;

        private int LanguageWeight = 5;
        private int ScriptWeight = 3;
        private int RegionWeight = 1;

        public string Language
        {
            get
            {
                return language;
            }
            set
            {
                if (value.IsNullOrWhiteSpace())
                {
                    throw new ArgumentNullException("language 不允许为空");
                }
                language = value.ToLower();
            }
        }
        public string Script
        {
            get
            {
                return script;
            }
            set
            {
                script = value.ToTitleCase();
            }
        }
        public string Region
        {
            get
            {
                return region;
            }
            set
            {
                region = value.ToUpper();
            }
        }

        public LanguageTag(string language, string script = "", string region = "")
        {
            if (language.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("language的值不能为空");
            }
            Language = language;
            Script = script;
            Region = region;
        }

        public static LanguageTag Parse(string value)
        {
            if (value.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("值为空，无法解析");
            }
            var constraintRegex = new Regex(@"^([a-z]{2,3})(?:\s*?-\s*?([a-z]{4}))?(?:\s*?-\s*?([a-z]{2}))?$", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            var matchResult = constraintRegex.Match(value);

            if (!matchResult.Success)
            {
                throw new ArgumentException("无法解析以下内容: " + value);
            }

            try
            {
                return new LanguageTag(matchResult.Groups[1].ToString(), matchResult.Groups[2].ToString(), matchResult.Groups[3].ToString());
            }
            catch (Exception ex)
            {
                throw new ArgumentException(String.Format("解析时发生错误:\n{0}\n无法完成解析", ex.Message));
            }

        }

        public int Match(LanguageTag dst)
        {
            int weight = 0;

            if (this.Language == dst.Language)
            {
                weight += LanguageWeight;
            }

            if ((!this.Script.IsNullOrWhiteSpace()) && this.Script == dst.Script)
            {
                weight += ScriptWeight;
            }

            if ((!this.Region.IsNullOrWhiteSpace()) && this.Region == dst.Region)
            {
                weight += RegionWeight;
            }

            return weight;
        }

        public static int Match(LanguageTag lt1, LanguageTag lt2)
        {
            return lt1.Match(lt2);
        }

        public override string ToString()
        {
            var builder = new StringBuilder(Language);

            if (!Script.IsNullOrWhiteSpace())
            {
                builder.Append("-" + Script);
            }

            if (!Region.IsNullOrWhiteSpace())
            {
                builder.Append("-" + Region);
            }

            return builder.ToString();
        }

        public bool Equals(LanguageTag other)
        {
            return this.Language == other.Language && this.Script == other.Script && this.Region == other.Region;
        }

        public override bool Equals(object obj)
        {
            return (obj is LanguageTag && Equals((LanguageTag)obj));
        }

        public static bool operator ==(LanguageTag lt1, LanguageTag lt2)
        {
            return Object.Equals(lt1, lt2);
        }

        public static bool operator !=(LanguageTag lt1, LanguageTag lt2)
        {
            return !Object.Equals(lt1, lt2);
        }

        public override int GetHashCode()
        {
            return language.GetHashCode() + script.GetHashCode() + region.GetHashCode();
        }
    }
}