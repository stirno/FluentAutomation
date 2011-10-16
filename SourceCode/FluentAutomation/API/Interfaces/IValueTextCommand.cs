using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Enumerations;

namespace FluentAutomation.API.Interfaces
{
    public interface IValueTextCommand
    {
        void In(string fieldSelector);
        void In(string fieldSelector, MatchConditions conditions);
        void In(MatchConditions conditions, params string[] fieldSelectors);
        void In(params string[] fieldSelectors);
    }
}
