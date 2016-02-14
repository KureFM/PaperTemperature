using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Core.I18N;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Web.Core.I18N.Tests
{
    [TestClass()]
    public class LanguageTagTests
    {
        [TestMethod()]
        public void ParseTest()
        {
            //全部正确参数
            var lt = LanguageTag.Parse("zh - hant -cn");
            Assert.AreEqual(lt.Language, "zh");
            Assert.AreEqual(lt.Script, "Hant");
            Assert.AreEqual(lt.Region, "CN");

            //部分正确参数
            lt = LanguageTag.Parse("zh -cn");
            Assert.AreEqual(lt.Language, "zh");
            Assert.AreEqual(lt.Region, "CN");

            //错误参数
            try
            {
                lt = LanguageTag.Parse("");
                Assert.Fail();
            }
            catch (Exception)
            {

            }
            try
            {
                lt = LanguageTag.Parse("hant -cn");
                Assert.Fail();
            }
            catch (Exception)
            {

            }
            try
            {
                lt = LanguageTag.Parse("zh - hant -");
                Assert.Fail();
            }
            catch (Exception)
            {

            }
        }

        [TestMethod()]
        public void EqualsTest()
        {
            var a = LanguageTag.Parse("zh-cn");
            var b = LanguageTag.Parse("zh-tw");
            var c = LanguageTag.Parse("zh-cn");
            if (a == b)
            {
                Assert.Fail();
            }
            if (a != c)
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void MatchTest()
        {
            var a = LanguageTag.Parse("zh-cn");
            var b = LanguageTag.Parse("zh-hans-cn");
            var c = LanguageTag.Parse("zh-tw");
            var d = LanguageTag.Parse("zh-hans");
            var e = LanguageTag.Parse("fr-FR");

            if (a.Match(b) != 6)
            {
                Assert.Fail();
            }

            if (b.Match(c) != 5)
            {
                Assert.Fail();
            }

            if (LanguageTag.Match(b, d) != 8)
            {
                Assert.Fail();
            }

            if (e.Match(a)!=0)
            {
                Assert.Fail();
            }
        }
    }
}
