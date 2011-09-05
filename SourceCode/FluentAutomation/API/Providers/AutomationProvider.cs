using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Enumerations;
using FluentAutomation.API.Interfaces;

namespace FluentAutomation.API.Providers
{
    public abstract class AutomationProvider
    {
        public abstract void Cleanup();

        public abstract ITextElement GetTextElement(string fieldSelector);

        public abstract ISelectElement GetSelectElement(string fieldSelector);

        public abstract IElement GetElement(string fieldSelector);

        public abstract IntPtr GetBrowserPointer();

        public abstract Uri GetUri();

        public string GetUrl()
        {
            return GetUri().ToString();
        }

        public void Navigate(string pageUrl)
        {
            Navigate(new Uri(pageUrl, UriKind.Absolute));
        }

        public abstract void Navigate(Uri pageUri);

        public abstract void Navigate(NavigateDirection navigationDirection);

        public abstract void SetBrowser(BrowserType browserType);

        public abstract void Wait(TimeSpan waitTime);

        public abstract void Wait(int seconds);
    }
}
