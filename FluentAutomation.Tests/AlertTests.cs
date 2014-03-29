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
            Config
            .OnAssertFailed((ex, state) =>
            {});
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

            //I.Expect.Class("red").Not.On("#id");
            //I.Expect.Count(1).Not.Of("li");

            With
                .WaitUntil(1)
                .WindowSize(800, 600)
            .Then
                .Wait(3)
                .Assert
                    .Not.Visible("#hiddenthing")
                    .Not.Exists("#halp");

            I.Assert.Not.True(() => false);
            I.Assert.Not.False(() => true);
            I.Assert.Not.Url("http://google.com");
            I.Assert.Not.Throws(() => "".ToString());

            I.Wait(5);

            //With.WaitUntil(2).Then.Assert.Exists("#wat");
        }
    }
}
