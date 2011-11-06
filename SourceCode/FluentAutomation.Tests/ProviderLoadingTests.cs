using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentAutomation.Tests
{
    [TestClass]
    public class ProviderLoadingTests : FluentAutomation.API.FluentTest
    {
        [TestMethod]
        public void Test()
        {
            I.Open("http://www.google.com/");
        }
    }
}
