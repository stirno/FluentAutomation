using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.API.ControlHandlers
{
    public class DraggedItemHandler
    {
        /*private Browser _browser = null;

        internal DraggedItemHandler(Browser browser)
        {
            _browser = browser;
        }

        public void To(string elementSelector)
        {
            var element = _browser.Child(Find.BySelector(elementSelector));
            System.Drawing.Rectangle dropBounds = element.NativeElement.GetElementBounds();
            To(dropBounds.X, dropBounds.Y);
        }

        public void To(int x, int y)
        {
            Point actualPoint = MouseControl.GetPointInBrowser(_browser, x, y);
            MouseControl.SetCursorPos(actualPoint.X, actualPoint.Y);
            MouseControl.MouseEvent(MouseControl.MouseEvent_LeftButtonUp, actualPoint.X, actualPoint.Y, 0, 0);
        }

        public void To(Point point)
        {
            To(point.X, point.Y);
        }*/
    }
}
