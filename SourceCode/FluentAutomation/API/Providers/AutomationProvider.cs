// <copyright file="AutomationProvider.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;
using FluentAutomation.API.Enumerations;
using FluentAutomation.API.Interfaces;

namespace FluentAutomation.API.Providers
{
    /// <summary>
    /// Automation Provider Abstract - Primary extensibility point
    /// </summary>
    public abstract class AutomationProvider
    {
        /// <summary>
        /// Gets or sets the screenshot path.
        /// </summary>
        /// <value>
        /// The screenshot path.
        /// </value>
        public string ScreenshotPath { get; set; }

        /// <summary>
        /// Provider cleanup.
        /// </summary>
        public abstract void Cleanup();

        /// <summary>
        /// Clicks the specified point (X, Y coordinates).
        /// </summary>
        /// <param name="point">The point.</param>
        public abstract void ClickPoint(API.Point point);

        /// <summary>
        /// Gets the text element matching the field selector and conditions.
        /// </summary>
        /// <param name="fieldSelector">The field selector.</param>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        public abstract ITextElement GetTextElement(string fieldSelector, MatchConditions conditions);

        /// <summary>
        /// Gets the select element matching the field selector and conditions.
        /// </summary>
        /// <param name="fieldSelector">The field selector.</param>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        public abstract ISelectElement GetSelectElement(string fieldSelector, MatchConditions conditions);

        /// <summary>
        /// Gets the element matching the field selector and conditions.
        /// </summary>
        /// <param name="fieldSelector">The field selector.</param>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        public abstract IElement GetElement(string fieldSelector, MatchConditions conditions);

        /// <summary>
        /// Gets the elements matching the field selector and conditions.
        /// </summary>
        /// <param name="fieldSelector">The field selector.</param>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        public abstract IElement[] GetElements(string fieldSelector, MatchConditions conditions);

        /// <summary>
        /// Gets the URI.
        /// </summary>
        /// <returns></returns>
        public abstract Uri GetUri();

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <returns></returns>
        public string GetUrl()
        {
            return GetUri().ToString();
        }

        /// <summary>
        /// Handles the alert dialog.
        /// </summary>
        public void HandleAlertDialog()
        {
            HandleAlertDialog(string.Empty);
        }

        /// <summary>
        /// Handles the alert dialog.
        /// </summary>
        /// <param name="expectedMessage">The expected message.</param>
        public abstract void HandleAlertDialog(string expectedMessage);

        /// <summary>
        /// Hovers over the point (X, Y coordinates).
        /// </summary>
        /// <param name="point">The point.</param>
        public abstract void HoverPoint(API.Point point);

        /// <summary>
        /// Navigates to the specified page URL.
        /// </summary>
        /// <param name="pageUrl">The page URL.</param>
        public void Navigate(string pageUrl)
        {
            Navigate(new Uri(pageUrl, UriKind.Absolute));
        }

        /// <summary>
        /// Navigates to the specified page URI.
        /// </summary>
        /// <param name="pageUri">The page URI.</param>
        public abstract void Navigate(Uri pageUri);

        /// <summary>
        /// Navigates the browser the specified direction.
        /// </summary>
        /// <param name="navigationDirection">The navigation direction.</param>
        public abstract void Navigate(NavigateDirection navigationDirection);

        /// <summary>
        /// Sets the browser to the specified type.
        /// </summary>
        /// <param name="browserType">Type of the browser.</param>
        public abstract void SetBrowser(BrowserType browserType);

        /// <summary>
        /// Takes the screenshot.
        /// </summary>
        public void TakeScreenshot()
        {
            TakeScreenshot(string.Format("{0}\\Exception-{1}.jpg", this.ScreenshotPath, DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss")));
        }

        /// <summary>
        /// Takes screenshot of the browser.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public abstract void TakeScreenshot(string fileName);

        /// <summary>
        /// Uploads the specified file name with the field specified.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fieldSelector">The field selector.</param>
        /// <param name="conditions">The conditions.</param>
        public abstract void Upload(string fileName, string fieldSelector, MatchConditions conditions);

        /// <summary>
        /// Waits the specified time.
        /// </summary>
        /// <param name="waitTime">The wait time.</param>
        public abstract void Wait(TimeSpan waitTime);

        /// <summary>
        /// Waits the specified number of seconds.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        public abstract void Wait(int seconds);
    }
}
