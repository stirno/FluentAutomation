using System;
using FluentAutomation;
using FluentAutomation.Tests;
using FluentAutomation.Tests.Native;
using Xunit;

namespace Tests
{
    public class SeleniumFirefox : SeleniumChrome
    {
        public SeleniumFirefox()
        {
            FluentAutomation.SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Firefox);
        }
    }
}