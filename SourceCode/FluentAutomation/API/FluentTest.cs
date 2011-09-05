using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Providers;

namespace FluentAutomation.API
{
    public abstract class FluentTest : IDisposable
    {
        public abstract ActionManager I { get; }

        public void Dispose()
        {
            I.Finish();
        }
    }
}
