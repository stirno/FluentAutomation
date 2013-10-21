using FluentAutomation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation
{
    public class BaseFluentTest : IDisposable
    {
        public ISyntaxProvider SyntaxProvider { get; set; }

        public void Dispose()
        {
            try
            {
                if (FluentSession.Current == null && this.SyntaxProvider != null)
                {
                    this.SyntaxProvider.Dispose();
                }

                if (Settings.MinimizeAllWindowsOnTestStart) Win32Magic.RestoreAllWindows();
            }
            catch { };
        }
    }
}
