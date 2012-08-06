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
            FluentAutomation.WatiN.Bootstrap(FluentAutomation.WatiN.Browser.InternetExplorer);
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

        [Fact]
        public void MouseClickOperations()
        {
            this.interactive.MouseClickOperations();
        }
    }
}
