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
                var provider = syntaxProvider as INativeActionSyntaxProvider;
                if (provider == null || provider.IsDisposed())
                {
                    // register types
                    FluentAutomation.Settings.Registration(this.Container);
                    syntaxProvider = this.Container.Resolve<ActionSyntaxProvider>();
                }

                return syntaxProvider as INativeActionSyntaxProvider;
            }
        }
    }
}