using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Automation = WatiN;

namespace FluentAutomation.WatiN
{
    public class AlertDialogHandler : Automation.Core.DialogHandlers.BaseDialogHandler
    {
        public override bool HandleDialog(Automation.Core.Native.Windows.Window window)
        {
            var button = GetOKButton(window);
            if (button != null)
            {
                button.Click();
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool CanHandleDialog(Automation.Core.Native.Windows.Window window)
        {
            return GetOKButton(window) != null;
        }

        private Automation.Core.Native.Windows.WinButton GetOKButton(Automation.Core.Native.Windows.Window window)
        {
            var windowButton = new Automation.Core.Native.InternetExplorer.WindowsEnumerator().GetChildWindows(window.Hwnd, w => w.ClassName == "Button" && new Automation.Core.Native.Windows.WinButton(w.Hwnd).Title == "OK").FirstOrDefault();
            if (windowButton == null)
                return null;
            else
                return new Automation.Core.Native.Windows.WinButton(windowButton.Hwnd);
        }
    }
}
