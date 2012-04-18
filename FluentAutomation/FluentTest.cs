using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class FluentTest : BaseFluentTest
    {
        public INativeActionSyntaxProvider I
        {
            get
            {
                if (syntaxProvider == null)
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