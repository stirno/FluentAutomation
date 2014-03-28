using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation
{
    public enum CommandType
    {
        Action,
        Expect,
        Assert,
        Wait,
        NoRetry
    }
}
