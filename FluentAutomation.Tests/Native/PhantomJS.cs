using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests
{
    public class PhantomJS : RepeatableNativeTest
    {
        public PhantomJS()
        {
            FluentAutomation.PhantomJS.Bootstrap();
        }

        [Fact]
        public void YUIDragDrop()
        {
            this.interactive.YUIDragDrop();
            this.interactive.I.Expect.Url("http://automation.apphb.com/interactive");
        }

        [Fact]
        public void Autocomplete_ExpectedResult()
        {
            this.forms.Autocomplete_ExpectedResult();
        }

        [Fact]
        public void CartEditor_BuyMotorcycles()
        {
            this.forms.CartEditor_BuyMotorcycles();
        }

        [Fact]
        public void MouseClickOperations()
        {
            this.interactive.MouseClickOperations();
        }
    }
}
