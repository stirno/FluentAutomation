// <copyright file="AutomationProvider.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;
using FluentAutomation.API.Enumerations;
using FluentAutomation.API.Interfaces;

namespace FluentAutomation.API.Providers
{
    public abstract class AutomationProvider
    {
        public abstract void Cleanup();

        public abstract void ClickPoint(API.Point point);

        public abstract ITextElement GetTextElement(string fieldSelector, MatchConditions conditions);

        public abstract ISelectElement GetSelectElement(string fieldSelector, MatchConditions conditions);

        public abstract IElement GetElement(string fieldSelector, MatchConditions conditions);

        public abstract IElement[] GetElements(string fieldSelector, MatchConditions conditions);

        public abstract Uri GetUri();

        public string GetUrl()
        {
            return GetUri().ToString();
        }

        public void HandleAlertDialog()
        {
            HandleAlertDialog(string.Empty);
        }

        public abstract void HandleAlertDialog(string expectedMessage);

        public abstract void HoverPoint(API.Point point);

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
