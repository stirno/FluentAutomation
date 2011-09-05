using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Automation = global::WatiN;
using System.Threading;
using FluentAutomation.API.Interfaces;

namespace FluentAutomation.WatiN
{
    public class AutomationProvider : FluentAutomation.API.Providers.AutomationProvider
    {
        private Automation.Core.Browser _browser = null;
        private API.Enumerations.BrowserType _browserType = API.Enumerations.BrowserType.InternetExplorer;

        public override void Cleanup()
        {
            _browser.Close();
            _browser = null;
        }

        public override IElement GetElement(string fieldSelector)
        {
            var wElement = _browser.Element(Automation.Core.Find.BySelector(fieldSelector));
            return new Element(wElement);
        }

        public override ISelectElement GetSelectElement(string fieldSelector)
        {
            var wElement = _browser.ElementOfType<Automation.Core.SelectList>(Automation.Core.Find.BySelector(fieldSelector));
            return new SelectElement(wElement);
        }

        public override ITextElement GetTextElement(string fieldSelector)
        {
            var wElement = _browser.ElementOfType<Automation.Core.TextField>(Automation.Core.Find.BySelector(fieldSelector));
            return new TextElement(wElement);
        }

        public override IntPtr GetBrowserPointer()
        {
            return _browser.NativeBrowser.hWnd;
        }

        public override Uri GetUri()
        {
            return _browser.Uri;
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

        public override void Wait(TimeSpan waitTime)
        {
            Thread.Sleep(waitTime);
        }

        public override void Wait(int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }

        private Automation.Core.Browser getCurrentBrowser()
        {
            switch (_browserType)
            {
                case API.Enumerations.BrowserType.InternetExplorer:
                    return new Automation.Core.IE(true);
                case API.Enumerations.BrowserType.Firefox:
                    return new Automation.Core.FireFox();
            }

            return null;
        }
    }
}
