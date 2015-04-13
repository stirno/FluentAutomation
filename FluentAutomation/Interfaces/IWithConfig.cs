using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Interfaces
{
    internal interface IWithConfig
    {
        IActionSyntaxProvider WithConfig(FluentSettings settings);
    }
}
