using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Interfaces
{
    public interface ISyntaxProvider : IDisposable
    {
        /// <summary>
        /// Used to determine if we've finished a test and need to create a new syntax provider
        /// </summary>
        bool IsDisposed();
    }
}
