using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentAutomation.Tests
{
    [TestClass]
    public class ProviderLoadingTests : FluentAutomation.API.FluentTest
    {
        [TestMethod]
        public void Test()
        {
            I.Open("http://www.google.com/");
        }

        [TestMethod]
        public void Test1()
        {
            I.Open("http://knockoutjs.com/examples/controlTypes.html");
            I.Repeat(i =>
            {
                I.Enter("Test" + i).In("input:eq(0)");
            }, 10);
        }
    }
}
