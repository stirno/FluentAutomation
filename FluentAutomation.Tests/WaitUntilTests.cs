using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests
{
    public class WaitUntilTests : FluentTest
    {
        public WaitUntilTests()
        {
            FluentAutomation.SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome);
        }

        [Fact]
        public void ItemExists()
        {
            I.Open(@"file:///C:/Projects/Testbed/WaitUntilTests.html");
            var sw = Stopwatch.StartNew();
            I.WaitUntil(() => I.Expect.Exists("#item-0"));
            I.Click("#item-0");
            sw.Stop();
        }
    }
}