using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.FieldHandlers;
using System.Windows.Automation;
using System.Runtime.InteropServices;
using FluentAutomation.API.Enumerations;
using System.Drawing;
using FluentAutomation.API.Providers;
using FluentAutomation.API.Interfaces;

namespace FluentAutomation.API
{
    public partial class ActionManager
    {
        private AutomationProvider _automation = null;
        private ExpectManager _expect = null;

        public ActionManager(AutomationProvider automationProvider)
        {
            _automation = automationProvider;
        }

        public void Click(string elementSelector)
        {
            var field = _automation.GetElement(elementSelector);
            field.Focus();
            field.Click();
        }

        public void Click(API.Point point)
        {
            _automation.ClickPoint(point);
        }

        public DraggedFieldHandler Drag(string fieldSelector)
        {
            return new DraggedFieldHandler(_automation, fieldSelector);
        }

        public TextFieldHandler Enter(string value)
        {
            return new TextFieldHandler(_automation, value);
        }

        public TextFieldHandler Enter(int value)
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
            var field = _automation.GetElement(elementSelector);
            field.Hover();
        }

        public void Hover(API.Point point)
        {

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

        public SelectFieldHandler Select(string value)
        {
            return new SelectFieldHandler(_automation, new string[] { value });
        }

        public SelectFieldHandler Select(int index)
        {
            return new SelectFieldHandler(_automation, new int[] { index });
        }

        public SelectFieldHandler Select(params string[] values)
        {
            return new SelectFieldHandler(_automation, values);
        }

        public SelectFieldHandler Select(params int[] indices)
        {
            return new SelectFieldHandler(_automation, indices);
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
