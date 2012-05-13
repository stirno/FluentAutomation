using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Native
{
    public class SeleniumChrome : RepeatableNativeTest
    {
        public SeleniumChrome()
        {
            FluentAutomation.SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome);
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

        [Fact]
        public void MSDNInputField()
        {
            this.forms.MSDNInputField();
        }

        [Fact]
        public void GoogleInputField()
        {
            this.forms.GoogleInputField();
        }
    }
}
