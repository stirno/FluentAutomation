using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    /// <summary>
    /// FluentTest - To be extended by tests targeting FluentAutomation. In the constructor, a user should call an appropriate bootstrap function from a FluentAutomation Provider.
    /// </summary>
    public class FluentTest : BaseFluentTest
    {
        /// <summary>
        /// Actions - Fluent's action functionality.
        /// </summary>
        public INativeActionSyntaxProvider I
        {
            get
            {
                var provider = SyntaxProvider as INativeActionSyntaxProvider;
                if (provider == null || provider.IsDisposed())
                {
                    this.Session.BootstrapTypeRegistration(FluentAutomation.Settings.Registration);
                    SyntaxProvider = this.Session.GetSyntaxProvider();
                }

                return SyntaxProvider as INativeActionSyntaxProvider;
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
                    session.RegisterSyntaxProvider<ActionSyntaxProvider>();
                }

                return session;
            }
        }
    }
}