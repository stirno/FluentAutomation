// <copyright file="AutomationProvider.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FluentAutomation.API;
using FluentAutomation.API.Enumerations;
using FluentAutomation.API.Exceptions;
using FluentAutomation.API.Interfaces;
using Automation = global::WatiN;

namespace FluentAutomation.WatiN
{
    public class AutomationProvider : FluentAutomation.API.Providers.AutomationProvider
    {
        private Automation.Core.Browser _browser = null;
        private API.Enumerations.BrowserType _browserType = API.Enumerations.BrowserType.InternetExplorer;
        private Automation.Core.DialogHandlers.AlertDialogHandler _alertDialogHandler = null;
        private List<string> _alertDialogMessages = new List<string>();

        public override void Cleanup()
        {
            _browser.Close();
            _browser = null;
        }

        public override void ClickPoint(API.Point point)
        {
            MouseControl.Click(point);
        }

        public override IElement GetElement(string fieldSelector, MatchConditions conditions)
        {
            var wElement = _browser.Element(Automation.Core.Find.BySelector(fieldSelector));
            ValidateElement(wElement, fieldSelector, conditions);
            
            return new Element(wElement);
        }

        public override IElement[] GetElements(string fieldSelector, MatchConditions conditions)
        {
            var elements = _browser.Elements.Filter(Automation.Core.Find.BySelector(fieldSelector));

            foreach (var element in elements)
            {
                ValidateElement(element, fieldSelector, conditions);
            }

            return elements.Select(e => new Element(e)).ToArray();
        }

        public override ISelectElement GetSelectElement(string fieldSelector, MatchConditions conditions)
        {
            var wElement = _browser.ElementOfType<Automation.Core.SelectList>(Automation.Core.Find.BySelector(fieldSelector));
            ValidateElement(wElement, fieldSelector, conditions);

            return new SelectElement(wElement);
        }

        public override ITextElement GetTextElement(string fieldSelector, MatchConditions conditions)
        {
            var wElement = _browser.ElementOfType<Automation.Core.TextField>(Automation.Core.Find.BySelector(fieldSelector));
            ValidateElement(wElement, fieldSelector, conditions);

            return new TextElement(wElement);
        }

        public override Uri GetUri()
        {
            return _browser.Uri;
        }

        public override void HandleAlertDialog(string expectedMessage)
        {
            using (new Automation.Core.DialogHandlers.UseDialogOnce(_browser.DialogWatcher, _alertDialogHandler))
            {
                _alertDialogHandler.WaitUntilExists();
                _alertDialogHandler.OKButton.Click();

                if (expectedMessage != string.Empty && !_alertDialogHandler.Message.Equals(expectedMessage, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new AssertException("Alert assertion failed. Expected message of [{0}] but actual message was [{1}].", expectedMessage, _alertDialogHandler.Message);
                }
            }
        }

        public override void HoverPoint(Point point)
        {
            MouseControl.SetPosition(point);
        }

        public override void Navigate(Uri pageUri)
        {
            if (_browser == null)
            {
                _browser = getCurrentBrowser();
            }

            _browser.GoTo(pageUri);
        }

        public override void Navigate(API.Enumerations.NavigateDirection navigationDirection)
        {
            switch (navigationDirection)
            {
                case API.Enumerations.NavigateDirection.Back:
                    _browser.Back();
                    break;
                case API.Enumerations.NavigateDirection.Forward:
                    _browser.Forward();
                    break;
            }
        }

        public override void SetBrowser(API.Enumerations.BrowserType browserType)
        {
            if (_browser != null)
            {
                throw new Exception("Browser Type can't be changed after it has been accessed.");
            }

            _browserType = browserType;
        }

        public override void TakeScreenshot(string fileName)
        {
            _browser.CaptureWebPageToFile(fileName);
        }
        
        public override void Upload(string fileName, string fieldSelector, MatchConditions conditions)
        {
            var handler = new Automation.Core.DialogHandlers.FileUploadDialogHandler(fileName);
            using (new Automation.Core.DialogHandlers.UseDialogOnce(_browser.DialogWatcher, handler))
            {
                IElement element = GetElement(fieldSelector, conditions);
                element.Click(ClickMode.Default);
            }
        }
        
        public override void Wait(TimeSpan waitTime)
        {
            Thread.Sleep(waitTime);
        }

        public override void Wait(int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }

        private void ValidateElement(Automation.Core.Element element, string fieldSelector, MatchConditions conditions)
        {
            if (conditions.HasFlag(MatchConditions.Visible))
            {
                if (element.Style.Display.ToLower().Contains("none") || element.Style.GetAttributeValue("visibility") == "hidden")
                {
                    throw new MatchConditionException(fieldSelector, MatchConditions.Visible);
                }
            }
            else if (conditions.HasFlag(MatchConditions.Hidden))
            {
                if (!element.Style.Display.ToLower().Contains("none") && !(element.Style.GetAttributeValue("visibility") == "visible"))
                {
                    throw new MatchConditionException(fieldSelector, MatchConditions.Hidden);
                }
            }
        }

        private Automation.Core.Browser getCurrentBrowser()
        {
            switch (_browserType)
            {
                case API.Enumerations.BrowserType.InternetExplorer:
                    // TODO: Calculate browser chrome height/width so we don't need fullscreen mode
                    Automation.Core.IE browser = new Automation.Core.IE(true);
                    ((SHDocVw.WebBrowser)browser.InternetExplorer).FullScreen = true;

                    // setup handler
                    _alertDialogHandler = new Automation.Core.DialogHandlers.AlertDialogHandler();
                    browser.AutoClose = true;
                    return browser;
                case API.Enumerations.BrowserType.Firefox:
                    throw new NotImplementedException("WatiN only supports Firefox with JSSH enabled. JSSH is not supported on versions newer than 4.0 so it has been disabled via this API.");
                default:
                    throw new NotImplementedException("WatiN only supports Internet Explorer. Switch to Selenium if you want to target other browsers.");
            }
        }
    }
}
