using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(ExpectAlertArguments))]
    public class ExpectAlert : ICommand
    {
        public void Execute(API.CommandManager manager, ICommandArguments arguments)
        {
            var args = (ExpectAlertArguments)arguments;

            if (args.Text != null)
            {
                manager.Expect.Alert(args.Text);
            }
            else
            {
                manager.Expect.Alert();
            }
        }
    }

    public class ExpectAlertArguments : ICommandArguments
    {
        public string Text { get; set; }
    }
}
