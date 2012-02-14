using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.Server.Model;
using FluentAutomation.Server.ViewModel;
using FluentAutomation.API.Enumerations;
using FluentAutomation.API;
using System.Threading;
using FluentAutomation.RemoteCommands;

namespace FluentAutomation.Server
{
    public class TestExecutionManager
    {
        protected TestDetails Details { get; set; }
        protected CommandManager Manager { get; set; }
        protected bool RequiresSTA { get; set; }

        public TestExecutionManager(TestDetails details)
        {
            this.Details = details;
            this.RemoteCommands = new List<RemoteCommandViewModel>();

            foreach (var command in details.RemoteCommands)
            {
                this.RemoteCommands.Add(new RemoteCommandViewModel
                {
                    CommandName = command.Key.GetType().Name,
                    RemoteCommand = command.Key,
                    RemoteCommandArguments = command.Value
                });
            }

            // new test class so we can grab the proper provider
            FluentTest testClass = new FluentTest();
            this.Manager = testClass.I;
            if (details.Browsers.Count > 0)
            {
                var selectedBrowser = details.Browsers[0];
                this.Manager.Use(details.Browsers[0]);
            }
        }

        public void Execute()
        {
            foreach (var cmd in this.RemoteCommands)
            {
                try
                {
                    cmd.RemoteCommand.Execute(this.Manager, cmd.RemoteCommandArguments);
                    RemoteCommandManager.PingbackStepCompleted(this.Details.AgentIdentifier, this.Details.UniqueTestRunIdentifier, this.Details.StepCompletionPingbackUri);
                }
                catch (Exception ex)
                {
                    RemoteCommandManager.PingbackStepFailed(this.Details.AgentIdentifier, this.Details.UniqueTestRunIdentifier, this.Details.StepCompletionPingbackUri, ex);
                }
            }

            this.Manager.Finish();
            RemoteCommandManager.PingbackTestCompleted(this.Details.AgentIdentifier, this.Details.UniqueTestRunIdentifier, this.Details.StepCompletionPingbackUri);
        }

        public List<RemoteCommandViewModel> RemoteCommands { get; set; }
    }
}
