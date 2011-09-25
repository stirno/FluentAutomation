// <copyright file="ActionManager.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;
using System.Linq.Expressions;
using FluentAutomation.API.Enumerations;
using FluentAutomation.API.Providers;

namespace FluentAutomation.API
{
    public class ActionManager
    {
        private AutomationProvider _automation = null;
        private ExpectManager _expect = null;

        public ActionManager(AutomationProvider automationProvider)
        {
            _automation = automationProvider;
        }

        public void Click(string elementSelector)
        {
            Click(elementSelector, MatchConditions.None);
        }

        public void Click(string elementSelector, MatchConditions conditions)
        {
            var field = _automation.GetElement(elementSelector, conditions);
            field.Focus();
            field.Click();
        }

        public void Click(API.Point point)
        {
            _automation.ClickPoint(point);
        }

        public FieldCommands.DragDrop Drag(string fieldSelector)
        {
            return new FieldCommands.DragDrop(_automation, fieldSelector);
        }

        public FieldCommands.Text Enter(string value)
        {
            return new FieldCommands.Text(_automation, value);
        }

        public FieldCommands.Text Enter(int value)
        {
            return Enter(value.ToString());
        }

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

        public void Finish()
        {
            _automation.Cleanup();
        }

        public void Hover(string elementSelector)
        {
            Hover(elementSelector, MatchConditions.None);
        }

        public void Hover(string elementSelector, MatchConditions conditions)
        {
            var field = _automation.GetElement(elementSelector, conditions);
            field.Hover();
        }

        public void Hover(API.Point point)
        {
            _automation.HoverPoint(point);
        }

        public void Navigate(NavigateDirection direction)
        {
            _automation.Navigate(direction);
        }
        
        public void Open(Uri pageUri)
        {
            _automation.Navigate(pageUri);
        }

        public void Open(string pageUrl)
        {
            Open(new Uri(pageUrl, UriKind.Absolute));
        }

        /// <summary>
        /// Uses Windows.Forms.SendKeys to send key events. See http://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys.aspx for details.
        /// </summary>
        /// <param name="keys"></param>
        public void Press(string keys)
        {
            ActionManager.SendKeys(keys);
        }

        public void Type(string value)
        {
            ActionManager.SendString(value);
        }

        public static void SendKeys(string keys)
        {
            System.Windows.Forms.SendKeys.SendWait(keys);
        }

        public static void SendString(string keys)
        {
            foreach (var chr in keys)
            {
                System.Windows.Forms.SendKeys.SendWait(chr.ToString());
            }
        }

        public FieldCommands.Select Select(Expression<Func<string, bool>> optionMatchingFunc)
        {
            return Select(optionMatchingFunc, SelectMode.Text);
        }

        public FieldCommands.Select Select(Expression<Func<string, bool>> optionMatchingFunc, SelectMode selectMode)
        {
            return new FieldCommands.Select(_automation, optionMatchingFunc, selectMode);
        }

        public FieldCommands.Select Select(string value)
        {
            return Select(value, SelectMode.Value);
        }

        public FieldCommands.Select Select(string value, SelectMode selectMode)
        {
            return new FieldCommands.Select(_automation, new string[] { value }, selectMode);
        }

        public FieldCommands.Select Select(params string[] values)
        {
            return Select(SelectMode.Value, values);
        }

        public FieldCommands.Select Select(SelectMode selectMode, params string[] values)
        {
            return new FieldCommands.Select(_automation, values, selectMode);
        }

        public FieldCommands.Select Select(int index)
        {
            return new FieldCommands.Select(_automation, new int[] { index }, SelectMode.Index);
        }

        public FieldCommands.Select Select(params int[] indices)
        {
            return new FieldCommands.Select(_automation, indices, SelectMode.Index);
        }

        public void Upload(string fileName, string fieldSelector)
        {
            Upload(fileName, fieldSelector, MatchConditions.Visible);
        }

        public void Upload(string fileName, string fieldSelector, MatchConditions conditions)
        {
            _automation.Upload(fileName, fieldSelector, conditions);
        }

        public void Use(BrowserType browserType)
        {
            _automation.SetBrowser(browserType);
        }

        public void Wait(int seconds)
        {
            _automation.Wait(seconds);
        }

        public void Wait(TimeSpan timeSpan)
        {
            _automation.Wait(timeSpan);
        }
    }
}
