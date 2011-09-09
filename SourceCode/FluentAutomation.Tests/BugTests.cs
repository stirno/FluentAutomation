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
    public class BugTests : FluentAutomation.WatiN.FluentTest
    {
        [TestMethod]
        public void Bug_1_CantExpectValueOnSelect()
        {
            I.Open("http://knockoutjs.com/examples/controlTypes.html");
            I.Select("Beta").From("select:eq(0)");
            I.Expect.This("Beta").In("select:eq(0)");
            I.Expect.Any("Alpha", "Beta").In("select:eq(0)");

            I.Select("Beta", "Gamma").From("select:eq(1)");
            I.Expect.All("Beta", "Gamma").In("select:eq(1)");
            I.Expect.Any("Beta").In("select:eq(1)");
        }

        [TestMethod]
        public void Bug_4_CantExpectValueOnInput()
        {
            I.Open("http://knockoutjs.com/examples/controlTypes.html");
            I.Enter("Test").In("input:eq(0)");
            I.Expect.This("Test").In("input:eq(0)");
        }
    }
}
