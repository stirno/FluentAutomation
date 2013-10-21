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
                var provider = SyntaxProvider as IRemoteActionSyntaxProvider;
                if (provider == null || provider.IsDisposed())
                {
                    // register types
                    this.Session.BootstrapTypeRegistration(FluentAutomation.Settings.Registration);
                    SyntaxProvider = this.Session.GetSyntaxProvider();
                }

                return SyntaxProvider as IRemoteActionSyntaxProvider;
            }
        }
        
        private FluentSession session = null;
        public FluentSession Session
        {
            get
            {
                if (session == null)
                {
                    session = new FluentSession();
                    session.RegisterSyntaxProvider<IRemoteActionSyntaxProvider>();
                }

                return session;
            }
        }
    }
}
