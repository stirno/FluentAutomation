using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class ConsoleLogger : ILogger
    {
       

        public void LogMessage(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }

        public void LogPartialMessage(string message, bool endLine = false, params object[] args)
        {
            if (endLine)
            {
                message += Environment.NewLine;
            }
            Console.Write(message, args);
        }


        public void LogException(Exception exception, string message, params object[] args)
        {
            Console.WriteLine(message, args);

            if (exception == null)
            {
                return; 
            }

            Console.WriteLine("--- EXCEPTION ----------------");
            Console.WriteLine("Message: {0}", exception.Message ?? "-");
            Console.WriteLine("StackTrace: {0}", exception.StackTrace ?? "-");

            LogInnerException(exception);
        }

        private void LogInnerException(Exception exception)
        {
            if (exception == null || exception.InnerException == null)
            {
                return;
            }

            Exception innerException = exception.InnerException;

            Console.WriteLine("--- INNER EXCEPTION ----------");
            Console.WriteLine("Message: {0}", innerException.Message ?? "-");
            Console.WriteLine("StackTrace: {0}", innerException.StackTrace ?? "-");

            LogInnerException(innerException);
        }
    }
}
