// <copyright file="FeatureTests.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAutomation.API.Enumerations;

namespace FluentAutomation.Tests
{
    [TestClass]
    public class FeatureTests : FluentAutomation.SeleniumWebDriver.FluentTest
    {
        [TestMethod]
        public void CssClassExpect()
        {
            I.Open("http://knockoutjs.com/examples/controlTypes.html");
            I.Expect.Class(".nogutter").On(".syntaxhighlighter");
        }

        [TestMethod]
        public void Navigate_BackForward()
        {
            I.Open("http://knockoutjs.com/examples/controlTypes.html");
            I.Open("http://knockoutjs.com/examples/betterList.html");
            I.Expect.Url("http://knockoutjs.com/examples/betterList.html");
            I.Navigate(NavigateDirection.Back);
            I.Expect.Url("http://knockoutjs.com/examples/controlTypes.html");
        }

        [TestMethod]
        public void Test()
        {
            I.Open("http://developer.yahoo.com/yui/examples/dragdrop/dd-groups.html");
            I.Drag("#pt1").To("#t2");
            I.Drag("#pt2").To("#t1");
            I.Drag("#pb1").To("#b1");
            I.Drag("#pb2").To("#b2");
            I.Drag("#pboth1").To("#b3");
            I.Drag("#pboth2").To("#b4");
            I.Drag("#pt1").To("#pt2");
            I.Drag("#pboth1").To("#pb2");
        }

        [TestInitialize]
        public void Setup()
        {
            //I.EnableRemoteExecution = true;
        }

        [TestCleanup]
        public void Execute()
        {
           // I.Execute(new Uri("http://localhost:10001/runtest", UriKind.Absolute));
        }

        [TestMethod]
        public void Drag_Drop_Test_Chrome()
        {
            I.Use(BrowserType.Chrome);
        }

        [TestMethod]
        public void TestFromGitHub()
        {
            // specify a browser, this is optional - WatiN targets IE and Selenium defaults to Firefox
            I.Use(BrowserType.InternetExplorer);
            I.Open("http://knockoutjs.com/examples/cartEditor.html");
            I.Select("Motorcycles").From(".liveExample tr select:eq(0)"); // Select by value/text
            I.Select(2).From(".liveExample tr select:eq(1)"); // Select by index
            I.Enter(6).In(".liveExample td.quantity input:eq(0)");
            I.Expect.Text("$197.70").In(".liveExample tr span:eq(1)");

            // add second product
            I.Click(".liveExample button:eq(0)");
            I.Select(1).From(".liveExample tr select:eq(2)");
            I.Select(4).From(".liveExample tr select:eq(3)");
            I.Enter(8).In(".liveExample td.quantity input:eq(1)");
            I.Expect.Text("$788.64").In(".liveExample tr span:eq(3)");

            // validate totals
            I.Expect.Text("$986.34").In("p.grandTotal span");

            // remove first product
            I.Click(".liveExample a:eq(0)");

            // validate new total
            I.Expect.Text("$788.64").In("p.grandTotal span");
        }

        [TestMethod]
        public void Test_FuncExpects()
        {
            I.Open("http://knockoutjs.com/examples/controlTypes.html");
            I.Expect.Url(x => x.AbsoluteUri.Contains("controlTypes.html"));
        }

        [TestMethod]
        public void Test_CountExpect()
        {
            I.Open("http://knockoutjs.com/examples/controlTypes.html");
            I.Expect.Count(2).Of(".syntaxhighlighter");
        }

        [TestMethod]
        public void Test_SelectFuncWithMode()
        {
            I.Open("http://www.htmlcodetutorial.com/linking/linking_famsupp_114.html");
            I.Select(x => x.Contains("Guide"), SelectMode.Text).From("select:eq(0)");
        }

        [TestMethod]
        public void TestGetNamesFromExpression()
        {
            var exprString = "((x, y, z, d) => x.Contains(\"test\")";

            var variablesSection = exprString.Substring(0, exprString.IndexOf("=>"))
                                             .Trim(' ', '(', ')')
                                             .Replace(" ","");
            var variables = variablesSection.Split(',');

            var expr = exprString.Substring(exprString.IndexOf("=>") + 2).Trim(' ', '(', ')');
        }

        [TestMethod]
        public void Test_RelativeClick()
        {
            I.Open("http://www.uploadify.com/demos/");
            I.Upload(@"C:\Users\Public\Pictures\Sample Pictures\Chrysanthemum.jpg", "#basic-demo", new API.Point { X = 2, Y = 90 });
            I.Expect.Text(x => x.Contains("File Size Error")).In("#file_uploadQueue .percentage");
            //I.ClickWithin("#basic-demo", new API.Point { X = 2, Y = -100 });
            //I.Click("#basic-demo", new API.Point { X = 2, Y = 90 });
        }

        [TestMethod]
        public void Test_ElementExpression()
        {
            I.Open("http://knockoutjs.com/");
            I.Expect.Element(x => x.GetAttributeValue("src") == "http://knockoutjs.com/img/feature-icons/declarative-bindings.png").In("li img:eq(0)");
        }
    }
}
