using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Automation;
using System.Drawing;

namespace FluentAutomation.API
{
    internal static class MouseControl
    {
        #region DllImports, ack!
        internal const int MouseEvent_LeftButtonDown = 0x002;
        internal const int MouseEvent_LeftButtonUp = 0x004;

        [DllImport("user32.dll")]
        internal static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll", EntryPoint = "mouse_event")]
        internal static extern void MouseEvent(int a, int x, int y, int d, int e);

        internal static Point GetPointInBrowser(IntPtr pointer, int pointX, int pointY)
        {
            AutomationElement element = null;
            element = AutomationElement.FromHandle(pointer);

            int actualX = (int)element.Current.BoundingRectangle.X + pointX + 10;
            int actualY = (int)element.Current.BoundingRectangle.Y + pointY + 55;

            return new Point { X = actualX, Y = actualY };
        }
        #endregion
    }
}
