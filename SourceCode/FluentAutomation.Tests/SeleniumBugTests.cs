// <copyright file="SeleniumBugTests.cs" author="Brandon Stirnaman">
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
    public class SeleniumBugTests : FluentAutomation.WatiN.FluentTest
    {
        [TestMethod]
        public void TestSelenium()
        {
            I.Open("http://knockoutjs.com/examples/cartEditor.html");
            I.Select("Motorcycles").From("#cartEditor tr select:eq(0)");
            I.Select("1957 Vespa GS150").From("#cartEditor tr select:eq(1)");
            I.Enter(6).Quickly.In("#cartEditor td.quantity input");

            I.Expect.Text("$197.70").In("#cartEditor tr span:eq(1)");
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
    }
}
