using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Automation;
using System.Drawing;

namespace FluentAutomation.API
{
    public static class MouseControl
    {
        public const int MouseEvent_LeftButtonDown = 0x002;
        public const int MouseEvent_LeftButtonUp = 0x004;

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll", EntryPoint = "mouse_event")]
        public static extern void MouseEvent(int a, int x, int y, int d, int e);

        public static Point GetPointInBrowser(API.Point point, int pointX, int pointY)
        {
            AutomationElement element = null;
            element = AutomationElement.FromPoint(new System.Windows.Point { X = point.X, Y = point.Y });

            int actualX = (int)element.Current.BoundingRectangle.X + pointX + 10;
            int actualY = (int)element.Current.BoundingRectangle.Y + pointY + 55;

            return new Point { X = actualX, Y = actualY };
        }

        public static void Click(API.Point point)
        {
            MouseEvent(MouseEvent_LeftButtonDown, point.X, point.Y, 0, 0);
            MouseEvent(MouseEvent_LeftButtonUp, point.X, point.Y, 0, 0);
        }

        public static void SetPosition(API.Point point)
        {
            SetCursorPos(point.X, point.Y);
        }
    }
}
