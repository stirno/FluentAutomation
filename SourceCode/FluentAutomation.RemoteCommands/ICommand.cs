using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API;

namespace FluentAutomation.RemoteCommands
{
    public interface ICommand
    {
        void Execute(CommandManager manager, ICommandArguments arguments);
    }
}
