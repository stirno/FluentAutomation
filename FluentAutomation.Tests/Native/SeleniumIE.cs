using System;
using FluentAutomation;
using FluentAutomation.Tests;
using Xunit;

namespace Tests
{
    public class SeleniumIE : RepeatableNativeTest
    {
        public SeleniumIE()
        {
            FluentAutomation.SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.InternetExplorer);
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