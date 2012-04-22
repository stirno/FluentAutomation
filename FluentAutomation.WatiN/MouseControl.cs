using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FluentAutomation
{
    public static class MouseControl
    {
        /// <summary>
        /// LeftButtonDown mouse event code
        /// </summary>
        public const int MouseEvent_LeftButtonDown = 0x002;

        /// <summary>
        /// LeftButtonUp mouse event code
        /// </summary>
        public const int MouseEvent_LeftButtonUp = 0x004;

        /// <summary>
        /// Sets the cursor position.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        /// <summary>
        /// Triggers mouse event.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        [DllImport("user32.dll", EntryPoint = "mouse_event")]
        public static extern void MouseEvent(int a, int x, int y, int d, int e);

        /// <summary>
        /// Clicks the specified point (X, Y coordinates).
        /// </summary>
        /// <param name="point">The point.</param>
        public static void Click(int x, int y)
        {
            SetPosition(x, y);
            MouseEvent(MouseEvent_LeftButtonDown, x, y, 0, 0);
            MouseEvent(MouseEvent_LeftButtonUp, x, y, 0, 0);
        }

        /// <summary>
        /// Sets the cursor position to the specified point (X, Y coordinates).
        /// </summary>
        /// <param name="point">The point.</param>
        public static void SetPosition(int x, int y)
        {
            SetCursorPos(x, y);
        }
    }
}
