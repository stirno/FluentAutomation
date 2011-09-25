using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Providers;
using FluentAutomation.API.Enumerations;

namespace FluentAutomation.API.ExpectCommands
{
    public class Count
    {
        private AutomationProvider _automation = null;
        private int _count = int.MinValue;

        public Count(AutomationProvider automation, int count)
        {
            _automation = automation;
            _count = count;
        }

        public void Of(string fieldSelector)
        {
            Of(fieldSelector, MatchConditions.None);
        }

        public void Of(string fieldSelector, MatchConditions conditions)
        {
            var elements = _automation.GetElements(fieldSelector, conditions);

            if (elements.Count() != _count)
            {
                throw new AssertException("Count assertion failed. Expected there to be [{0}] elements matching [{1}]. Actual count is [{2}]", _count, fieldSelector, elements.Count());
            }
        }
    }
}
