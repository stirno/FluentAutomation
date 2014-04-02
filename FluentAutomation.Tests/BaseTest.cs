using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests
{
    /// <summary>
    /// Base Test that opens the test to the AUT
    /// </summary>
    public class BaseTest : FluentTest<IWebDriver>
    {
        public string SiteUrl { get { return "http://localhost:38043/"; } }
        
        public BaseTest()
        {
            FluentSession.EnableStickySession();
            Config.WaitUntilTimeout(TimeSpan.FromMilliseconds(2000));

            // Create Page Objects
            this.InputsPage = new Pages.InputsPage(this);
            this.AlertsPage = new Pages.AlertsPage(this);
            this.ScrollingPage = new Pages.ScrollingPage(this);
            this.TextPage = new Pages.TextPage(this);
            this.DragPage = new Pages.DragPage(this);
            
            // Default tests use chrome and load the site
            FluentAutomation.SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome);//, SeleniumWebDriver.Browser.InternetExplorer, SeleniumWebDriver.Browser.Firefox);
            I.Open(SiteUrl);
        }

        public Pages.InputsPage InputsPage = null;
        public Pages.AlertsPage AlertsPage = null;
        public Pages.ScrollingPage ScrollingPage = null;
        public Pages.TextPage TextPage = null;
        public Pages.DragPage DragPage = null;
    }

    public class AssertBaseTest : BaseTest
    {
        public AssertBaseTest()
            : base()
        {
            Config.OnExpectFailed((ex, state) =>
            {
                // For the purpose of these tests, allow expects to throw (break test)
                throw ex;
            });
        }
    }
}
