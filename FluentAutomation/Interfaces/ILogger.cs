using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Interfaces
{
    public interface ILogger
    {
        void LogMessage(string message, params object[] args);

        void LogPartialMessage(string message, bool endLine = false, params object[] args);

        void LogException(Exception exception, string message, params object[] args);
    }
}
