// <copyright file="ActionManager.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;
using System.Linq.Expressions;
using FluentAutomation.API.Enumerations;
using FluentAutomation.API.Providers;

namespace FluentAutomation.API
{
    /// <summary>
    /// The "I" in I.Click, the primary interaction class
    /// </summary>
    public class ActionManager
    {
        private AutomationProvider _automation = null;
        private ExpectManager _expect = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionManager"/> class.
        /// </summary>
        /// <param name="automationProvider">The automation provider.</param>
        public ActionManager(AutomationProvider automationProvider)
        {
            _automation = automationProvider;
        }

        /// <summary>
        /// Clicks the specified element selector.
        /// </summary>
        /// <param name="elementSelector">The element selector.</param>
        public void Click(string elementSelector)
        {
            Click(elementSelector, ClickMode.Default);
        }

        public void Click(string elementSelector, ClickMode clickMode)
        {
            Click(elementSelector, clickMode, MatchConditions.None);
        }

        public void Click(string elementSelector, ClickMode clickMode, MatchConditions conditions)
        {
            var field = _automation.GetElement(elementSelector, conditions);
            field.Focus();
            field.Click(clickMode);
        }

        /// <summary>
        /// Clicks the specified point (X, Y coordinates).
        /// </summary>
        /// <param name="point">The point.</param>
        public void Click(API.Point point)
        {
            _automation.ClickPoint(point);
        }

        /// <summary>
        /// Drags the specified field selector.
        /// </summary>
        /// <param name="fieldSelector">The field selector.</param>
        /// <returns></returns>
        public FieldCommands.DragDrop Drag(string fieldSelector)
        {
            return new FieldCommands.DragDrop(_automation, fieldSelector);
        }

        /// <summary>
        /// Enters the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public FieldCommands.Text Enter(string value)
        {
            return new FieldCommands.Text(_automation, value);
        }

        /// <summary>
        /// Enters the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public FieldCommands.Text Enter(int value)
        {
            return Enter(value.ToString());
        }

        /// <summary>
        /// Gets the ExpectManager
        /// </summary>
        public ExpectManager Expect
        {
            get
            {
                if (_expect == null)
                {
                    _expect = new ExpectManager(_automation);
                }

                return _expect;
            }
        }

        /// <summary>
        /// Finishes a test, called within Dispose()
        /// </summary>
        public void Finish()
        {
            _automation.Cleanup();
        }

        /// <summary>
        /// Hovers over the specified element selector.
        /// </summary>
        /// <param name="elementSelector">The element selector.</param>
        public void Hover(string elementSelector)
        {
            Hover(elementSelector, MatchConditions.None);
        }

        /// <summary>
        /// Hovers over the specified element selector that matches the conditions.
        /// </summary>
        /// <param name="elementSelector">The element selector.</param>
        /// <param name="conditions">The conditions.</param>
        public void Hover(string elementSelector, MatchConditions conditions)
        {
            var field = _automation.GetElement(elementSelector, conditions);
            field.Hover();
        }

        /// <summary>
        /// Hovers over the specified point (X, Y coordinates).
        /// </summary>
        /// <param name="point">The point.</param>
        public void Hover(API.Point point)
        {
            _automation.HoverPoint(point);
        }

        /// <summary>
        /// Navigates the browser the specified direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public void Navigate(NavigateDirection direction)
        {
            _automation.Navigate(direction);
        }

        /// <summary>
        /// Opens the specified page URI.
        /// </summary>
        /// <param name="pageUri">The page URI.</param>
        public void Open(Uri pageUri)
        {
            _automation.Navigate(pageUri);
        }

        /// <summary>
        /// Opens the specified page URL.
        /// </summary>
        /// <param name="pageUrl">The page URL.</param>
        public void Open(string pageUrl)
        {
            Open(new Uri(pageUrl, UriKind.Absolute));
        }

        /// <summary>
        /// Presses the specified keys using ActionManager.SendKeys()
        /// </summary>
        /// <param name="keys">The keys.</param>
        public void Press(string keys)
        {
            ActionManager.SendKeys(keys);
        }

        /// <summary>
        /// Types the specified value using ActionManager.SendString()
        /// </summary>
        /// <param name="value">The value.</param>
        public void Type(string value)
        {
            ActionManager.SendString(value);
        }

        /// <summary>
        /// Windows.Forms.SendKeys wrapper. See http://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys.aspx for details.
        /// </summary>
        /// <param name="keys">The keys.</param>
        public static void SendKeys(string keys)
        {
            System.Windows.Forms.SendKeys.SendWait(keys);
        }

        /// <summary>
        /// Windows.Forms.SendKeys wrapper for typing entire strings.
        /// </summary>
        /// <param name="keys">The keys.</param>
        public static void SendString(string keys)
        {
            foreach (var chr in keys)
            {
                System.Windows.Forms.SendKeys.SendWait(chr.ToString());
            }
        }

        /// <summary>
        /// Selects the specified options where the expression returns true.
        /// </summary>
        /// <param name="optionExpression">The option expression.</param>
        /// <returns></returns>
        public FieldCommands.Select Select(Expression<Func<string, bool>> optionExpression)
        {
            return Select(optionExpression, SelectMode.Text);
        }

        /// <summary>
        /// Selects the specified options where the expression returns true.
        /// </summary>
        /// <param name="optionExpression">The option expression.</param>
        /// <param name="selectMode">The select mode.</param>
        /// <returns></returns>
        public FieldCommands.Select Select(Expression<Func<string, bool>> optionExpression, SelectMode selectMode)
        {
            return new FieldCommands.Select(_automation, optionExpression, selectMode);
        }

        /// <summary>
        /// Selects the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public FieldCommands.Select Select(string value)
        {
            return Select(value, SelectMode.Value);
        }

        /// <summary>
        /// Selects the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="selectMode">The select mode.</param>
        /// <returns></returns>
        public FieldCommands.Select Select(string value, SelectMode selectMode)
        {
            return new FieldCommands.Select(_automation, new string[] { value }, selectMode);
        }

        /// <summary>
        /// Selects the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public FieldCommands.Select Select(params string[] values)
        {
            return Select(SelectMode.Value, values);
        }

        /// <summary>
        /// Selects the specified values.
        /// </summary>
        /// <param name="selectMode">The select mode.</param>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public FieldCommands.Select Select(SelectMode selectMode, params string[] values)
        {
            return new FieldCommands.Select(_automation, values, selectMode);
        }

        /// <summary>
        /// Selects the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public FieldCommands.Select Select(int index)
        {
            return new FieldCommands.Select(_automation, new int[] { index }, SelectMode.Index);
        }

        /// <summary>
        /// Selects the specified indices.
        /// </summary>
        /// <param name="indices">The indices.</param>
        /// <returns></returns>
        public FieldCommands.Select Select(params int[] indices)
        {
            return new FieldCommands.Select(_automation, indices, SelectMode.Index);
        }

        /// <summary>
        /// Uploads the specified file name with the field specified.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fieldSelector">The field selector.</param>
        public void Upload(string fileName, string fieldSelector)
        {
            Upload(fileName, fieldSelector, MatchConditions.Visible);
        }

        /// <summary>
        /// Uploads the specified file name with the field specified.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fieldSelector">The field selector.</param>
        /// <param name="conditions">The conditions.</param>
        public void Upload(string fileName, string fieldSelector, MatchConditions conditions)
        {
            _automation.Upload(fileName, fieldSelector, conditions);
        }

        /// <summary>
        /// Uses the specified browser type.
        /// </summary>
        /// <param name="browserType">Type of the browser.</param>
        public void Use(BrowserType browserType)
        {
            _automation.SetBrowser(browserType);
        }

        /// <summary>
        /// Waits the specified number of seconds.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        public void Wait(int seconds)
        {
            _automation.Wait(seconds);
        }

        /// <summary>
        /// Waits the specified time.
        /// </summary>
        /// <param name="timeSpan">The time span.</param>
        public void Wait(TimeSpan timeSpan)
        {
            _automation.Wait(timeSpan);
        }
    }
}
