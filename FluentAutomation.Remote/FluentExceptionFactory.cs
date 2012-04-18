using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.Exceptions;

namespace FluentAutomation
{
    public class FluentExceptionFactory
    {
        public static FluentException Create(string exceptionTypeName, string message)
        {
            var expectFailedException = typeof(FluentAutomation.Exceptions.FluentExpectFailedException).ToString();

            if (exceptionTypeName == expectFailedException)
            {
                return new FluentExpectFailedException(message);
            }
            else
            {
                return new FluentException(message);
            }
        }
    }
}
