using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Remote
{
    public class RemoteTest : FluentTest
    {
        public RemoteTest()
        {
            FluentAutomation.SeleniumWebDriver.Bootstrap(new Uri("http://localhost:9515"), SeleniumWebDriver.Browser.Chrome);
        }

        [Fact]
        public void TestOp()
        {
            I.Open("http://www.bing.com");
        }
    }
}
