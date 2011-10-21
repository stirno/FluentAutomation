using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace FluentAutomation.RemoteCommands
{
    public static class Guard
    {
        public static void ArgumentNotNullForCommand<T>(object argument)
        {
            if (argument == null)
            {
                throw new InvalidCommandException<T>();
            }
            else if (argument.GetType() == typeof(string))
            {
                if (string.IsNullOrEmpty(argument.ToString()))
                {
                    throw new InvalidCommandException<T>();
                }
            }
        }

        public static void ArgumentNotNullForCommand<T>(params object[] arguments)
        {
            int nullCount = 0;

            foreach (var arg in arguments)
            {
                if (arg == null)
                {
                    nullCount++;
                }
                else if (arg.GetType() == typeof(string))
                {
                    if (string.IsNullOrEmpty(arg.ToString()))
                    {
                        nullCount++;
                    }
                }
            }

            if (nullCount == arguments.Count())
            {
                throw new InvalidCommandException<T>();
            }
        }
        
        public static void ArgumentExpressionTrueForCommand<T>(Expression<Func<bool>> expression)
        {
            var compiledFunc = expression.Compile();
            if (!compiledFunc())
                throw new InvalidCommandException<T>();
        }
    }
}
