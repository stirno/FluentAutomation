using System;
using FluentAutomation.Tests.Pages;
using OpenQA.Selenium;

namespace FluentAutomation.Tests
{
    /// <summary>
    /// Base Test that opens the test to the AUT.
    /// </summary>
    public class BaseTest : FluentTest<IWebDriver>
    {
        protected static string SiteUrl => "http://localhost:38043/";

        protected BaseTest()
        {
            FluentSession.EnableStickySession();
            Config.WaitUntilTimeout(TimeSpan.FromMilliseconds(1000));

            // Create Page Objects
            InputsPage = new InputsPage(this);
            AlertsPage = new AlertsPage(this);
            ScrollingPage = new ScrollingPage(this);
            TextPage = new TextPage(this);
            DragPage = new DragPage(this);
            SwitchPage = new SwitchPage(this);
            
            // Default tests use chrome and load the site.
            SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.InternetExplorer); // Other options: Chrome, Firefox
            I.Open(SiteUrl);
        }

        protected InputsPage InputsPage { get; }
        protected AlertsPage AlertsPage { get; }
        protected ScrollingPage ScrollingPage { get; }
        protected TextPage TextPage { get; }
        protected DragPage DragPage { get; }
        protected SwitchPage SwitchPage { get; }
    }

    public class AssertBaseTest : BaseTest
    {
        protected AssertBaseTest()
        {
            // For the purpose of these tests, allow expects to throw (break test).
            Config.OnExpectFailed((ex, state) => throw ex);
        }
    }
}
