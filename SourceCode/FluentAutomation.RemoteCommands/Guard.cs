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
            if (argument.GetType() == typeof(string))
            {
                if (string.IsNullOrEmpty(argument.ToString()))
                {
                    throw new InvalidCommandException<T>();
                }
            }
            else if (argument == null)
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
