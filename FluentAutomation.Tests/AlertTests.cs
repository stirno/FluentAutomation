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
            I.Assert.Text("ASP.NET1").Not.In("h1");
            I.Assert.Text("ASP.NET").In("h1");

            I.Assert.Not.Visible("#hiddenthing");

            I.Assert.Not.Exists("#halp");
            I.Assert.Not.True(() => false);
            I.Assert.Not.False(() => true);
            I.Assert.Not.Url("http://google.com");
            I.Assert.Not.Throws(() => "".ToString());
        }
    }
}
