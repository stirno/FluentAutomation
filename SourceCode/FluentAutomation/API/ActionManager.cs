using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;
using FluentAutomation.API.ControlHandlers;
using System.Windows.Automation;
using System.Runtime.InteropServices;
using FluentAutomation.API.Enumerations;
using System.Drawing;

namespace FluentAutomation.API
{
    public partial class ActionManager
    {
        private ExpectManager _expect = null;
        private Browser _browser = null;
        private BrowserType _browserType = BrowserType.InternetExplorer;

        private Browser browser {
            get
            {
                if (_browser == null) 
                {
                    if (_browserType == BrowserType.InternetExplorer)
                    {
                        _browser = new WatiN.Core.IE(true);
                    }
                    else if (_browserType == BrowserType.Firefox)
                    {
                        _browser = new WatiN.Core.FireFox();
                    }
                }
                return _browser;
            }
        }

        internal ActionManager()
        {
        }

        public virtual void Use(BrowserType browserType)
        {
            _browserType = browserType;
        }

        public virtual void Open(Uri pageUri)
        {
            browser.GoTo(pageUri);
            browser.WaitForComplete();
        }

        public virtual void Open(string pageUrl)
        {
            browser.GoTo(pageUrl);
            browser.WaitForComplete();
        }

        public ExpectManager Expect
        {
            get
            {
                if (_expect == null)
                {
                    _expect = new ExpectManager(browser);
                }

                return _expect;
            }

            set
            {
                _expect = value;
            }
        }

        public virtual TextFieldHandler Enter(Func<string> valueFunc)
        {
            return Enter(valueFunc());
        }

        public virtual TextFieldHandler Enter(string value)
        {
            return new TextFieldHandler(browser, value);
        }

        public virtual TextFieldHandler Enter(int value)
        {
            return new TextFieldHandler(browser, value.ToString());
        }

        public virtual SelectListHandler Select(string value)
        {
            return new SelectListHandler(browser, value);
        }

        public virtual SelectListHandler Select(params string[] values)
        {
            return new SelectListHandler(browser, values);
        }

        public SelectListHandler Select(int index)
        {
            return new SelectListHandler(browser, index);
        }

        public virtual SelectListHandler Select(params int[] indices)
        {
            return new SelectListHandler(browser, indices);
        }

        public virtual void Click(int pointX, int pointY)
        {
            Point actualPoint = MouseControl.GetPointInBrowser(browser, pointX, pointY);
            MouseControl.SetCursorPos(actualPoint.X, actualPoint.Y);
            MouseControl.MouseEvent(MouseControl.MouseEvent_LeftButtonDown, actualPoint.X, actualPoint.Y, 0, 0);
            MouseControl.MouseEvent(MouseControl.MouseEvent_LeftButtonUp, actualPoint.X, actualPoint.Y, 0, 0);
        }

        public virtual void Click(Point point)
        {
            Click(point.X, point.Y);
        }

        public virtual void Click(string elementSelector)
        {
            var element = browser.Child(Find.BySelector(elementSelector));
            element.Focus();
            element.Click();
        }

        public virtual void Hover(int pointX, int pointY)
        {
            Point actualPoint = MouseControl.GetPointInBrowser(browser, pointX, pointY);
            MouseControl.SetCursorPos(actualPoint.X, actualPoint.Y);
        }

        public virtual void Hover(Point point)
        {
            Hover(point.X, point.Y);
        }

        public virtual void Hover(string elementSelector)
        {
            var element = browser.Child(Find.BySelector(elementSelector));
            element.MouseEnter();
            element.NativeElement.GetElementBounds();
        }

        public virtual DraggedItemHandler Drag(string elementSelector)
        {
            var element = browser.Child(Find.BySelector(elementSelector));
            System.Drawing.Rectangle bounds = element.NativeElement.GetElementBounds();
            Point actualPoint = MouseControl.GetPointInBrowser(browser, bounds.X, bounds.Y);
            MouseControl.SetCursorPos(actualPoint.X, actualPoint.Y);
            MouseControl.MouseEvent(MouseControl.MouseEvent_LeftButtonDown, actualPoint.X, actualPoint.Y, 0, 0);

            return new DraggedItemHandler(browser);
        }

        public virtual void Wait(TimeSpan waitTime)
        {
            System.Threading.Thread.Sleep(waitTime);
        }

        public virtual void Finish()
        {
            browser.Close();
        }
    }
}
