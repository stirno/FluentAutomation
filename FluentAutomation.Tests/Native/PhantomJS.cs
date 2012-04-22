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
    }
}
