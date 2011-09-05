using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.ControlHandlers;
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

        public ActionManager(AutomationProvider automationProvider)
        {
            _automation = automationProvider;
        }

        public void Use(BrowserType browserType)
        {
            _automation.SetBrowser(browserType);
        }

        public void Open(Uri pageUri)
        {
            _automation.Navigate(pageUri);
        }

        public void Open(string pageUrl)
        {
            Open(new Uri(pageUrl, UriKind.Absolute));
        }

        public void Navigate(NavigateDirection direction)
        {
            _automation.Navigate(direction);
        }
        
        public TextFieldHandler Enter(string value)
        {
            return new TextFieldHandler(_automation, value);
        }

        public TextFieldHandler Enter(int value)
        {
            return Enter(value.ToString());
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

        public void Click(string elementSelector)
        {
            var field = _automation.GetElement(elementSelector);
            field.Focus();
            field.Click();
        }

        public void Hover(string elementSelector)
        {
            var field = _automation.GetElement(elementSelector);
            field.Hover();
        }

        public void Finish()
        {
            _automation.Cleanup();
        }

        private ExpectManager _expect = null;
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
        /*
        public virtual DraggedItemHandler Drag(string elementSelector)
        {
            var element = _browser.Child(Find.BySelector(elementSelector));
            System.Drawing.Rectangle bounds = element.NativeElement.GetElementBounds();
            Point actualPoint = MouseControl.GetPointInBrowser(_browser, bounds.X, bounds.Y);
            MouseControl.SetCursorPos(actualPoint.X, actualPoint.Y);
            MouseControl.MouseEvent(MouseControl.MouseEvent_LeftButtonDown, actualPoint.X, actualPoint.Y, 0, 0);

            return new DraggedItemHandler(_browser);
        }*/
    }
}
