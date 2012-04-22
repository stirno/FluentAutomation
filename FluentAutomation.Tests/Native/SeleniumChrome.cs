using System;
using FluentAutomation;
using FluentAutomation.Tests;
using Xunit;

namespace Tests
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
    }
}