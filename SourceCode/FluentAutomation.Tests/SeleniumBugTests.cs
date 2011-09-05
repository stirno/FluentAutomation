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
            I.Open("http://www.google.com");
            I.Enter("knockoutjs").In("#lst-ib");
        }
    }
}
