using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WrLib.Utils.StringExpander.Core;

namespace WrLib.Utils.StringExpander.Test
{
    [TestClass]
    public class StringExpanderUnitTest
    {
        [TestMethod]
        public void TestBasicFunction()
        {
            var dict = new Dictionary<string, string>();
            dict["name"] = "Smock";
            dict["phone"] = "180087763334";
            dict["info"] = "$$$(name):$(phone)$$";
            dict["kak"] = "$$$$$$$$";

            IStringExpander sx = new StringExpander(dict);

            Assert.AreEqual("$$$$", sx.Expand("$(kak)"));
            Assert.AreEqual("$Smock:180087763334$", sx.Expand("$(info)"));
            Assert.AreEqual(sx.Expand("$(info)"), sx.Resolve("info"));
            
        }
    }
}
