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
        public string SiteUrl { get { return "http://wbtstr.net-testbed.dt-dev1.mirabeau.nl/"; } }
        
        public BaseTest()
        {
            FluentSession.EnableStickySession();
            Config.WaitUntilTimeout(TimeSpan.FromMilliseconds(1000));

            // Create Page Objects
            this.InputsPage = new Pages.InputsPage(this);
            this.AlertsPage = new Pages.AlertsPage(this);
            this.ScrollingPage = new Pages.ScrollingPage(this);
            this.TextPage = new Pages.TextPage(this);
            this.DragPage = new Pages.DragPage(this);
            this.SwitchPage = new Pages.SwitchPage(this);
            
            // Default tests use chrome and load the site
            //FluentAutomation.SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome); //, SeleniumWebDriver.Browser.InternetExplorer, SeleniumWebDriver.Browser.Firefox);

            // Test browserstack local
            var _capabilities = new Dictionary<string, object>();
            SeleniumWebDriver.EnableBrowserStackLocal("***REMOVED***");
            _capabilities.Add("browserstack.local", "true");
            _capabilities.Add("browserstack.user", "***REMOVED***");
            _capabilities.Add("browserstack.key", "***REMOVED***");
            _capabilities.Add("browserstack.debug", "true");
            SeleniumWebDriver.Bootstrap(new Uri("http://hub.browserstack.com/wd/hub/"), _capabilities);

            I.Open(SiteUrl);
        }

        public Pages.InputsPage InputsPage = null;
        public Pages.AlertsPage AlertsPage = null;
        public Pages.ScrollingPage ScrollingPage = null;
        public Pages.TextPage TextPage = null;
        public Pages.DragPage DragPage = null;
        public Pages.SwitchPage SwitchPage = null;
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
