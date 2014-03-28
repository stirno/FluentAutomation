using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests
{
    [TestClass]
    public class AlertTests : FluentTest<IWebDriver>
    {
        public AlertTests()
        {
            FluentAutomation.SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome);
        }

        [TestMethod]
        public void CanHandleAlert()
        {
            I.Open("http://localhost:1474/Home/Index");
            I.Assert.Text("what is the answer to everything?").In(Alert.Message);
            I.Enter("this is a test").In(Alert.Input);
            I.Click(Alert.OK);

            I.Assert.Text("ASP.NET").In("h1");
        }
    }
}
