using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Interfaces;
using System.Threading;
using OpenQA.Selenium;
using System.Drawing;

namespace FluentAutomation.SeleniumWebDriver
{
    public class AutomationProvider : FluentAutomation.API.Providers.AutomationProvider
    {
        private IWebDriver _driver = null;
        private API.Enumerations.BrowserType _browserType = API.Enumerations.BrowserType.Firefox;

        public override void Cleanup()
        {
            _driver.Manage().Cookies.DeleteAllCookies();
            _driver.Quit();
        }

        public override void ClickPoint(API.Point point)
        {
            (new OpenQA.Selenium.Interactions.Actions(_driver)).MoveToElement(_driver.FindElement(By.TagName("body"))).MoveByOffset(point.X, point.Y).Click().Perform();
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

        public override Uri GetUri()
        {
            return new Uri(_driver.Url, UriKind.Absolute);
        }

        public override void HoverPoint(API.Point point)
        {
            (new OpenQA.Selenium.Interactions.Actions(_driver)).MoveToElement(_driver.FindElement(By.TagName("body"))).MoveByOffset(point.X, point.Y).Perform();
        }

        public override void Navigate(Uri pageUri)
        {
            if (_driver == null)
            {
                _driver = getCurrentDriver();
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
            if (_driver != null)
            {
                throw new Exception("Browser Type can't be changed after it has been accessed.");
            }

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

        private IWebDriver getCurrentDriver()
        {
            OpenQA.Selenium.Remote.RemoteWebDriver driver = null;

            switch (_browserType)
            {
                case API.Enumerations.BrowserType.InternetExplorer:
                    driver = new OpenQA.Selenium.IE.InternetExplorerDriver();
                    break;
                case API.Enumerations.BrowserType.Firefox:
                    driver = new OpenQA.Selenium.Firefox.FirefoxDriver();
                    break;
            }

            return driver;
        }
    }
}
