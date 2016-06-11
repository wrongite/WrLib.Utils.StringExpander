/*
 * Copyright 2016 Nattapon Artsarikorn
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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
            dict["info"] = "$(name):$(phone)";

            IStringExpander sx = new StringExpander(dict);

            // The escape characters for $ is $$.
            Assert.AreEqual("Value of $(info) is 'Smock:180087763334'", sx.Expand("Value of $$(info) is '$(info)'"));
            
        }

        [TestMethod]
        public void TestRecursiveKeyTolerance()
        {
            var dict = new Dictionary<string, string>();
            dict["A"] = "A is [$(A)]";
            dict["B"] = "B is not [$(C)]";
            dict["C"] = "C is not [$(B)]";

            IStringExpander sx = new StringExpander(dict);

            Assert.AreEqual("Value of $(A) is [A is [$(A)]]", sx.Expand("Value of $$(A) is [$(A)]"));
            Assert.AreEqual("Value of $(B) is [B is not [$(C)]]", sx.Expand("Value of $$(B) is [$(B)]"));
            Assert.AreEqual("Value of $(C) is [C is not [$(B)]]", sx.Expand("Value of $$(C) is [$(C)]"));
        }
    }
}
