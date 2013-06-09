using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Interfaces
{
    public interface IActionProvider
    {
        void Act(Action action, bool waitableAction = true);
    }
}
