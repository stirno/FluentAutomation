using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Config.OnAssertFailed((ex, state) =>
            {
                Trace.WriteLine("Error in assert, source looks like:");
                Trace.WriteLine("");
                Trace.WriteLine(state.Source);
            });
        }

        [TestMethod]
        public void CanHandleAlert()
        {
            I.Open("http://localhost:1474/Home/Index");
            I.Click("a[href='/Home/Contact']");
            I.Switch.Window("Contact - My ASP.NET Application");
            I.Assert.Text("Contact.").In("h2");
            I.Switch.Window();
            I.Assert.Not.Text("ASP.NET").In("h1");
        }
    }
}
