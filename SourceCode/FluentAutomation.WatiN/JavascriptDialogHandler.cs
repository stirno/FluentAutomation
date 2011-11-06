using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.DialogHandlers;
using WatiN.Core.Native.InternetExplorer;
using WatiN.Core.Native.Windows;

namespace FluentAutomation.WatiN
{
    // Borrowed from Julian Jelfs, good stuff!
    public class JavaScriptAlertDialogHandler : BaseDialogHandler
    {
        private readonly Action<string> _onAlert;

        public JavaScriptAlertDialogHandler(Action<string> onAlert)
        {
            _onAlert = onAlert;
        }

        public override bool HandleDialog(Window window)
        {
            if (CanHandleDialog(window))
            {
                _onAlert(window.Message);
                new WinButton(GetOKButtonId(), window.Hwnd).Click();
                return true;
            }
            return false;
        }

        public override bool CanHandleDialog(Window window)
        {
            return (window.StyleInHex == "94C801C5" && !ButtonWithId1Exists(window.Hwnd));
        }

        private static int GetOKButtonId()
        {
            return 2;
        }

        protected static bool ButtonWithId1Exists(IntPtr windowHwnd)
        {
            var button = new WinButton(1, windowHwnd);
            return button.Exists();
        }
    }

}