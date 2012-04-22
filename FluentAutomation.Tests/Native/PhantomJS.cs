using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests
{
    class PhantomJS : RepeatableNativeTest
    {
        public PhantomJS()
        {
            FluentAutomation.PhantomJS.Bootstrap();
        }

        [Fact]
        public void YUIDragDrop()
        {
            this.interactive.YUIDragDrop();
        }
    }
}
