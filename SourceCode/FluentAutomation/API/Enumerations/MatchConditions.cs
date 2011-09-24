using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.API.Enumerations
{
    [Flags]
    public enum MatchConditions
    {
        None = 0,
        Visible = 1,
        Hidden = 2
    }
}
