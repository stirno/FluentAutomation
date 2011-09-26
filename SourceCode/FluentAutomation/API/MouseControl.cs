// <copyright file="MouseControl.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System.Runtime.InteropServices;

namespace FluentAutomation.API
{
    /// <summary>
    /// Win32 MouseControl events
    /// </summary>
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
        public static void Click(API.Point point)
        {
            MouseEvent(MouseEvent_LeftButtonDown, point.X, point.Y, 0, 0);
            MouseEvent(MouseEvent_LeftButtonUp, point.X, point.Y, 0, 0);
        }

        /// <summary>
        /// Sets the cursor position to the specified point (X, Y coordinates).
        /// </summary>
        /// <param name="point">The point.</param>
        public static void SetPosition(API.Point point)
        {
            SetCursorPos(point.X, point.Y);
        }
    }
}
