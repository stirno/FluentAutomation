using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FluentAutomation
{
    public static class Win32Magic
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        private static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam);

        private const int WM_COMMAND = 0x111;
        private const int MIN_ALL = 419;
        private const int MIN_ALL_UNDO = 416;

        public static void MinimizeAllWindows()
        {
            IntPtr handle = FindWindow("Shell_TrayWnd", null);
            SendMessage(handle, WM_COMMAND, (IntPtr)MIN_ALL, IntPtr.Zero);
        }

        public static void RestoreAllWindows()
        {
            IntPtr handle = FindWindow("Shell_TrayWnd", null);
            SendMessage(handle, WM_COMMAND, (IntPtr)MIN_ALL_UNDO, IntPtr.Zero);
        }
    }
}
