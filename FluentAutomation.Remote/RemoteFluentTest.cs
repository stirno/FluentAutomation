using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class RemoteFluentTest : BaseFluentTest
    {
        public IRemoteActionSyntaxProvider I
        {
            get
            {
                if (syntaxProvider == null)
                {
                    // register types
                    FluentAutomation.Settings.Registration(this.Container);
                    syntaxProvider = this.Container.Resolve<RemoteActionSyntaxProvider>();
                }

                return syntaxProvider as IRemoteActionSyntaxProvider;
            }
        }
    }
}
