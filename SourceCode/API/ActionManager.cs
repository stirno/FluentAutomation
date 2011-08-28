using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;
using FluentAutomation.API.ControlHandlers;
using System.Windows.Automation;
using System.Runtime.InteropServices;
using FluentAutomation.API.Enumerations;

namespace FluentAutomation.API
{
    public class ActionManager
    {
        private ExpectManager _expect = null;
        private Browser _browser = null;

        internal ActionManager()
        {
        }

        public void Use(BrowserType browserType)
        {
            if (browserType == BrowserType.InternetExplorer)
            {
                _browser = new WatiN.Core.IE(true);
            }
            else if (browserType == BrowserType.Firefox)
            {
                _browser = new WatiN.Core.FireFox();
            }
        }

        public void Open(Uri pageUri)
        {
            _browser.GoTo(pageUri);
            _browser.WaitForComplete();
        }

        public void Open(string pageUrl)
        {
            _browser.GoTo(pageUrl);
            _browser.WaitForComplete();
        }

        public ExpectManager Expect
        {
            get
            {
                if (_expect == null)
                {
                    _expect = new ExpectManager(_browser);
                }

                return _expect;
            }

            set
            {
                _expect = value;
            }
        }

        public TextFieldHandler Enter(Func<string> valueFunc)
        {
            return Enter(valueFunc());
        }

        public TextFieldHandler Enter(string value)
        {
            return new TextFieldHandler(_browser, value);
        }

        public SelectListHandler Select(string value)
        {
            return new SelectListHandler(_browser, value);
        }

        public SelectListHandler Select(params string[] values)
        {
            return new SelectListHandler(_browser, values);
        }

        public SelectListHandler Select(int index)
        {
            return new SelectListHandler(_browser, index);
        }

        public SelectListHandler Select(params int[] indices)
        {
            return new SelectListHandler(_browser, indices);
        }

        public void Click(double pointX, double pointY)
        {
            Point actualPoint = GetActualPoint(pointX, pointY);
            SetCursorPos(actualPoint.X, actualPoint.Y);
            MouseEvent(MouseEvent_LeftButtonDown, actualPoint.X, actualPoint.Y, 0, 0);
            MouseEvent(MouseEvent_LeftButtonUp, actualPoint.X, actualPoint.Y, 0, 0);
        }

        public void Click(Point point)
        {
            Click(point.X, point.Y);
        }

        public void Click(string elementSelector)
        {
            var element = _browser.Child(Find.BySelector(elementSelector));
            element.Focus();
            element.Click();
        }

        public void Hover(double pointX, double pointY)
        {
            Point actualPoint = GetActualPoint(pointX, pointY);
            SetCursorPos(actualPoint.X, actualPoint.Y);
        }

        public void Hover(Point point)
        {
            Hover(point.X, point.Y);
        }

        public void Hover(string elementSelector)
        {
            var element = _browser.Child(Find.BySelector(elementSelector));
            element.MouseEnter();
        }

        public void Wait(TimeSpan waitTime)
        {
            System.Threading.Thread.Sleep(waitTime);
        }

        private Point GetActualPoint(double pointX, double pointY)
        {
            AutomationElement element = null;
            if (_browser != null)
            {
                element = AutomationElement.FromHandle(_browser.NativeBrowser.hWnd);

                double actualX = element.Current.BoundingRectangle.X + pointX;
                double actualY = element.Current.BoundingRectangle.Y + pointY;

                return new Point { X = actualX, Y = actualY };
            }

            return null;
        }

        #region DllImports, ack!
        private const int MouseEvent_LeftButtonDown = 0x002;
        private const int MouseEvent_LeftButtonUp = 0x004;

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(double x, double y);

        [DllImport("user32.dll", EntryPoint = "mouse_event")]
        private static extern void MouseEvent(int a, double x, double y, int d, int e);
        #endregion

        public void Finish()
        {
            _browser.Close();
        }
    }
}
