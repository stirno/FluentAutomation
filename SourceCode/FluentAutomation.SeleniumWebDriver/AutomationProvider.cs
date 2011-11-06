// <copyright file="AutomationProvider.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;
using System.Linq;
using System.Threading;
using FluentAutomation.API;
using FluentAutomation.API.Enumerations;
using FluentAutomation.API.Exceptions;
using FluentAutomation.API.Interfaces;
using OpenQA.Selenium;

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
            _driver = null;
        }

        public override void ClickWithin(string selector, Point point)
        {
            var container = _driver.FindElement(BySizzle.CssSelector(selector));

            (new OpenQA.Selenium.Interactions.Actions(_driver))
                .MoveToElement(container, point.X, point.Y)
                .Click()
                .Build()
                .Perform();
        }

        public override void ClickPoint(API.Point point)
        {
            this.ClickWithin("body", point);
        }

        public override ITextElement GetTextElement(string fieldSelector, MatchConditions conditions)
        {
            var element = _driver.FindElement(BySizzle.CssSelector(fieldSelector));
            ValidateElement(element, fieldSelector, conditions);

            return new TextElement(_driver, element, fieldSelector);
        }

        public override ISelectElement GetSelectElement(string fieldSelector, MatchConditions conditions)
        {
            var element = _driver.FindElement(BySizzle.CssSelector(fieldSelector));
            ValidateElement(element, fieldSelector, conditions);

            return new SelectElement(_driver, element, fieldSelector);
        }

        public override IElement GetElement(string fieldSelector, MatchConditions conditions)
        {
            var element = _driver.FindElement(BySizzle.CssSelector(fieldSelector));
            ValidateElement(element, fieldSelector, conditions);

            return new Element(_driver, element, fieldSelector);
        }

        public override IElement[] GetElements(string fieldSelector, MatchConditions conditions)
        {
            var elements = _driver.FindElements(BySizzle.CssSelector(fieldSelector));
            foreach (var element in elements)
            {
                ValidateElement(element, fieldSelector, conditions);
            }

            return elements.Select(e => new Element(_driver, e, fieldSelector)).ToArray();
        }

        public override Uri GetUri()
        {
            return new Uri(_driver.Url, UriKind.Absolute);
        }

        public override void HandleAlertDialog(string expectedMessage)
        {
            var alert = _driver.SwitchTo().Alert();
            var message = alert.Text;
            alert.Accept();

            if (expectedMessage != string.Empty && !message.Equals(expectedMessage, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new AssertException("Alert assertion failed. Expected message of [{0}] but actual message was [{1}].", expectedMessage, message);
            }
        }

        public override void HoverWithin(string selector, Point point)
        {
            (new OpenQA.Selenium.Interactions.Actions(_driver))
                .MoveToElement(_driver.FindElement(By.TagName(selector)), point.X, point.Y)
                .Build()
                .Perform();
        }

        public override void HoverPoint(API.Point point)
        {
            this.HoverWithin("body", point);
        }

        public override void Navigate(Uri pageUri)
        {
            if (_driver == null)
            {
                _driver = getCurrentDriver();
            }
            
            _driver.Navigate().GoToUrl(pageUri);
            BySizzle.EnsureSizzleIsLoaded((IJavaScriptExecutor)_driver);
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

        public override void TakeScreenshot(string fileName)
        {
            var image = ((ITakesScreenshot)_driver).GetScreenshot();
            image.SaveAsFile(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        public override void Upload(string fileName, string fieldSelector, API.Point offset, MatchConditions conditions)
        {
            if (_browserType == BrowserType.InternetExplorer) throw new FeatureNotImplementedException("SeleniumWebDriver+InternetExplorer File Upload");

            var element = _driver.FindElement(BySizzle.CssSelector(fieldSelector));
            var t = System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                // Dirty I know.. Need to guarantee the dialog opens before we start sending key events.
                int sleepTime = 0;
                switch (_browserType)
                {
                    case BrowserType.Firefox:
                        sleepTime = 1000;
                        break;
                    case BrowserType.Chrome:
                        sleepTime = 1500;
                        break;
                }

                Thread.Sleep(sleepTime);
                CommandManager.SendString(fileName + "~");
            }, System.Threading.Tasks.TaskCreationOptions.LongRunning);

            if (offset == null)
            {
                element.Click();
            }
            else
            {
                this.ClickWithin(fieldSelector, offset);
            }

            t.Wait();
        }

        public override void Wait(TimeSpan waitTime)
        {
            Thread.Sleep(waitTime);
        }

        public override void Wait(int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }

        private void ValidateElement(IWebElement element, string fieldSelector, MatchConditions conditions)
        {
            if (conditions.HasFlag(MatchConditions.Visible))
            {
                if (!element.Displayed)
                {
                    throw new MatchConditionException(fieldSelector, MatchConditions.Visible);
                }
            }
            else if (conditions.HasFlag(MatchConditions.Hidden))
            {
                if (element.Displayed)
                {
                    throw new MatchConditionException(fieldSelector, MatchConditions.Hidden);
                }
            }
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
                case API.Enumerations.BrowserType.Chrome:
                    driver = new OpenQA.Selenium.Chrome.ChromeDriver();
                    break;
            }

            driver.Manage().Cookies.DeleteAllCookies();

            return driver;
        }
    }
}
