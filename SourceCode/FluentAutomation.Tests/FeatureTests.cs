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
    public class FeatureTests : FluentAutomation.WatiN.FluentTest
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
    }
}
