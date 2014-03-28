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
            //I.Switch.Frame("iframe");
            I.Click("a[href='/Home/Contact']");
            I.Switch.Window("Contact - My ASP.NET Application");
            I.Assert.Text("Contact.").In("h2");
            I.Switch.Window();
            I.Assert.Text("ASP.NET1").In("h1");
        }
    }
}
