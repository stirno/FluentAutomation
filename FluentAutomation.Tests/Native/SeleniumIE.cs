using System;
using FluentAutomation;
using FluentAutomation.Tests;
using FluentAutomation.Tests.Native;
using Xunit;

namespace Tests
{
    public class SeleniumIE : SeleniumChrome
    {
        public SeleniumIE()
        {
            FluentAutomation.SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.InternetExplorer);
        }
    }
}