using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Interfaces;
using System.Threading;
using OpenQA.Selenium;

namespace FluentAutomation.SeleniumWebDriver
{
    public class AutomationProvider : FluentAutomation.API.Providers.AutomationProvider
    {
        private IWebDriver _driver = null;
        private API.Enumerations.BrowserType _browserType = API.Enumerations.BrowserType.Firefox;

        public override void Cleanup()
        {
            _driver.Close();
        }

        public override ITextElement GetTextElement(string fieldSelector)
        {
            return new TextElement(_driver, _driver.FindElement(BySizzle.CssSelector(fieldSelector)), fieldSelector);
        }

        public override ISelectElement GetSelectElement(string fieldSelector)
        {
            return new SelectElement(_driver, _driver.FindElement(BySizzle.CssSelector(fieldSelector)), fieldSelector);
        }

        public override IElement GetElement(string fieldSelector)
        {
            return new Element(_driver, _driver.FindElement(BySizzle.CssSelector(fieldSelector)), fieldSelector);
        }

        public override IntPtr GetBrowserPointer()
        {
            throw new NotImplementedException();
        }

        public override Uri GetUri()
        {
            return new Uri(_driver.Url, UriKind.Absolute);
        }

        public override void Navigate(Uri pageUri)
        {
            if (_driver == null)
            {
                _driver = getCurrentBrowser();
            }

            _driver.Navigate().GoToUrl(pageUri);
        }

        public override void Navigate(API.Enumerations.NavigateDirection navigationDirection)
        {
            switch (navigationDirection)
            {
                case API.Enumerations.NavigateDirection.Back:
                    _driver.Navigate().Back();
                    break;
                case API.Enumerations.NavigateDirection.Forward:
                    _driver.Navigate().Forward();
                    break;
            }
        }

        public override void SetBrowser(API.Enumerations.BrowserType browserType)
        {
            _browserType = browserType;
        }

        public override void Wait(TimeSpan waitTime)
        {
            Thread.Sleep(waitTime);
        }

        public override void Wait(int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }

        private IWebDriver getCurrentBrowser()
        {
            switch (_browserType)
            {
                case API.Enumerations.BrowserType.InternetExplorer:
                    return new OpenQA.Selenium.IE.InternetExplorerDriver();
                case API.Enumerations.BrowserType.Firefox:
                    return new OpenQA.Selenium.Firefox.FirefoxDriver();
            }

            return null;
        }
    }
}
