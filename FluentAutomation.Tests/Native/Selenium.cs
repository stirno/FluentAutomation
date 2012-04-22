using System;
using FluentAutomation;
using FluentAutomation.Tests;
using Xunit;

namespace Tests
{
    public class Selenium : RepeatableNativeTest
    {
        public Selenium()
        {
            FluentAutomation.SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Firefox);
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