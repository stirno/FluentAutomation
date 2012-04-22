using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FluentAutomation.Tests
{
    public class WatiN : RepeatableNativeTest
    {
        public WatiN()
        {
            FluentAutomation.WatiN.Bootstrap(FluentAutomation.WatiN.Browsers.InternetExplorer);
        }

        [Fact]
        public void InteractiveTests_YUIDragDrop()
        {
            this.interactive.YUIDragDrop();
        }
    }
}
