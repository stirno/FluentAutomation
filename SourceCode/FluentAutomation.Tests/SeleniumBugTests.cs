using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAutomation.API.Enumerations;

namespace FluentAutomation.Tests
{
    [TestClass]
    public class SeleniumBugTests : FluentAutomation.SeleniumWebDriver.SeleniumWebDriverTest
    {
        [TestMethod]
        public void TestSelenium()
        {
            I.Use(BrowserType.Chrome);
            I.Open("http://knockoutjs.com/examples/cartEditor.html");
            I.Select("Motorcycles").From("#cartEditor tr select:eq(0)");
            I.Select("1957 Vespa GS150").From("#cartEditor tr select:eq(1)");
            I.Enter(6).Quickly.In("#cartEditor td.quantity input");

            I.Expect.This("$197.70").In("#cartEditor tr span:eq(1)");
        }
    }
}
