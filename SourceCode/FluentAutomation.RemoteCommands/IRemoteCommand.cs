using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API;

namespace FluentAutomation.RemoteCommands
{
    public interface IRemoteCommand
    {
        void Execute(CommandManager manager, IRemoteCommandArguments arguments);
    }
}
