using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Providers;

namespace FluentAutomation.API.FieldHandlers
{
    public class DraggedFieldHandler
    {
        private AutomationProvider _automation = null;

        internal DraggedFieldHandler(AutomationProvider browser)
        {
            _automation = browser;
        }

        public void To(string elementSelector)
        {
            var element = _automation.GetElement(elementSelector);
            System.Drawing.Rectangle dropBounds = element.GetElementBounds();
            To(dropBounds.X, dropBounds.Y);
        }

        public void To(int x, int y)
        {
            Point actualPoint = MouseControl.GetPointInBrowser(_automation.GetBrowserPointer(), x, y);
            MouseControl.SetCursorPos(actualPoint.X, actualPoint.Y);
            MouseControl.MouseEvent(MouseControl.MouseEvent_LeftButtonUp, actualPoint.X, actualPoint.Y, 0, 0);
        }

        public void To(Point point)
        {
            To(point.X, point.Y);
        }
    }
}
