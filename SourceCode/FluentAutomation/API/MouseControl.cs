using System.Runtime.InteropServices;

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
