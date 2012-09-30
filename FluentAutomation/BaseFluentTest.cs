using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation
{
    public class BaseFluentTest : IDisposable
    {
        private TinyIoC.TinyIoCContainer container = null;
        public TinyIoC.TinyIoCContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = new TinyIoC.TinyIoCContainer();
                    if (Settings.MinimizeAllWindowsOnTestStart) Win32Magic.MinimizeAllWindows();
                }

                return container;
            }
        }

        protected IDisposable syntaxProvider = null;

        public void Dispose()
        {
            try
            {
                if (this.syntaxProvider != null) this.syntaxProvider.Dispose();
                if (Settings.MinimizeAllWindowsOnTestStart) Win32Magic.RestoreAllWindows();
            }
            catch { };
        }
    }
}
