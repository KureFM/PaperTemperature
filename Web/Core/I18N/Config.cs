using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Web.Core.I18N
{

    /// <summary>
    /// 支持语言配置节点
    /// </summary>
    public class SupportedLanguageSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public LanguageElementCollection Language
        {
            get
            {
                return (LanguageElementCollection)this[""];
            }
        }

        [ConfigurationProperty("defaultLanguage", IsRequired = true)]
        public string DefaultLanguage
        {
            get
            {
                return (string)this["defaultLanguage"];
            }
        }
    }


    /// <summary>
    /// 语言节点
    /// </summary>
    public class LanguageElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                var language = (string)this["name"];
                if (!LanguageValidator(language))
                {
                    throw new ArgumentException(String.Format("{0} 不是被支持的语言", language));
                }
                return language;
            }
        }

        [ConfigurationProperty("fallback", IsRequired = false, IsKey = false, DefaultValue = "true")]
        public bool Fallback
        {
            get
            {
                return (bool)this["fallback"];
            }
        }

        private bool LanguageValidator(string name)
        {
            foreach (CultureTypes ct in Enum.GetValues(typeof(CultureTypes)))
            {
                var cis = CultureInfo.GetCultures(ct);
                if (cis.Length != 0)
                {
                    foreach (var ci in cis)
                    {
                        if (ci.Name.ToLower() == name.ToLower())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }


    /// <summary>
    /// 语言节点集合
    /// </summary>
    [ConfigurationCollection(typeof(LanguageElement), AddItemName = "language")]
    public class LanguageElementCollection : ConfigurationElementCollection
    {
        protected override string ElementName
        {
            get
            {
                return "language";
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new LanguageElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((LanguageElement)element).Name;
        }
    }
}