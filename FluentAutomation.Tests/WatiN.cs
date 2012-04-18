using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FluentAutomation.Tests
{
    public class WatiN : FluentTest
    {
        public WatiN()
        {
            FluentAutomation.WatiN.Bootstrap(FluentAutomation.WatiN.Browsers.InternetExplorer);
        }

        [Fact]
        public void TestMethod1()
        {
            I.Open("http://knockoutjs.com/examples/cartEditor.html");
            I.FindMultiple(".liveExample tr select:eq(0)")();
        }
    }
}
