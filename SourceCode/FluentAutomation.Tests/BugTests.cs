// <copyright file="BugTests.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAutomation.API.Enumerations;

namespace FluentAutomation.Tests
{
    [TestClass]
    public class BugTests : FluentAutomation.SeleniumWebDriver.FluentTest
    {
        public override void Setup()
        {
            this.ScreenshotOnAssertException = false;
            this.ScreenshotPath = @"C:\Users\stirno\Pictures\TestScreenshots";
        }
        
        [TestMethod]
        public void Bug_1_CantExpectValueOnSelect()
        {
            //I.Use(BrowserType.Chrome);
            //I.Record();
            I.Open("http://knockoutjs.com/examples/controlTypes.html");
            I.Select(x => x.Contains("Be")).From("select:eq(0)");
            //I.Select("Beta", SelectMode.Value).From("select:eq(0)");
            I.Expect.Text(x => (x.Contains("ta") && x.Contains("Be"))).In("select:eq(0)", MatchConditions.Visible);
            I.Expect.Text("Beta").In("select:eq(0)", MatchConditions.Visible);
            I.Expect.Any("Alpha", "Beta").In("select:eq(0)");

            //I.Select(x => x.Length > 4).From("select:eq(1)");
            I.Select("Alpha", "Gamma").From("select:eq(1)");
            I.Expect.All(SelectMode.Value, "Alpha", "Gamma").In("select:eq(1)");
            I.Expect.Any(SelectMode.Text, "Alp3ha", "Alpha").In("select:eq(1)");
            //I.PlayWith(BrowserType.InternetExplorer);
        }

        [TestMethod]
        public void Bug_4_CantExpectValueOnInput()
        {
            I.Open("http://knockoutjs.com/examples/controlTypes.html");
            I.Enter("Test21").In("input:eq(0)");
            I.Expect.Value("Test21").In("input:eq(1)");
        }

        [TestMethod]
        public void Bug_14_ExpectEmptyString()
        {
            I.Open("http://knockoutjs.com/examples/controlTypes.html");
            I.Enter(string.Empty).In("input:eq(0)");
            I.Expect.Value(string.Empty).In("input:eq(0)");
        }

        [TestMethod]
        public void Bug_19_URLAssertion()
        {
            I.Open("http://knockoutjs.com/examples/controlTypes.html?returnUrl=http%3a%2f%2fknockoutjs.com%3a80");
            I.Expect.Url("http://knockoutjs.com/examples/controlTypes.html?returnUrl=http://knockoutjs.com:80");
            I.Expect.Url(x => x.AbsoluteUri.Length > 0);
            I.Expect.Url(x => x.AbsoluteUri != null);
        }

        [TestMethod]
        public void Bug_AlertDialog()
        {
            I.Use(BrowserType.InternetExplorer);
            I.Open("http://www.quackit.com/javascript/javascript_alert_box.cfm");
            I.Click("input[type='button']:eq(0)");
            I.Expect.Alert();
        }

        [TestMethod]
        public void Bug_FileUpload()
        {
            I.Open("http://encodable.com/uploaddemo/");
            I.Upload(@"c:\Users\Public\Public Pictures\Sample Pictures\Chrysanthemum.jpg", "input[type='file']");
        }

        [TestMethod]
        public void Bug_41_ExpectClassStringFormatIssue()
        {
            I.Open("http://datinix.net/fartest.html");
            I.Expect.Class("test").On("h1");
        }
    }
}
