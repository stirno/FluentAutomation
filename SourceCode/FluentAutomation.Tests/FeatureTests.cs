using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAutomation.API.Enumerations;

namespace FluentAutomation.Tests
{
    [TestClass]
    public class FeatureTests : FluentAutomation.API.TestClass
    {
        [TestMethod]
        public void CssClassExpect()
        {
            I.Use(BrowserType.InternetExplorer);
            I.Open("http://knockoutjs.com/examples/controlTypes.html");
            I.Expect.Class(".nogutter").On(".syntaxhighlighter");
        }
    }
}
