using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(ExpectUrlArguments))]
    public class ExpectUrl : IRemoteCommand
    {
        public void Execute(API.CommandManager manager, IRemoteCommandArguments arguments)
        {
            var args = (ExpectUrlArguments)arguments;

            Guard.ArgumentNotNullForCommand<ExpectUrl>(args.URL);

            if (args.URLExpression == null)
            {
                manager.Expect.Url(args.URL);
            }
            else
            {
                manager.Expect.Url(args.URLExpression);
            }
        }
    }

    public class ExpectUrlArguments : IRemoteCommandArguments
    {
        public string URL { get; set; }
        public Expression<Func<Uri, bool>> URLExpression { get; set; }
    }
}
