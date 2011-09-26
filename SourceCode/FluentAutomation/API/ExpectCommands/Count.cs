// <copyright file="Count.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System.Linq;
using FluentAutomation.API.Enumerations;
using FluentAutomation.API.Exceptions;
using FluentAutomation.API.Providers;

namespace FluentAutomation.API.ExpectCommands
{
    /// <summary>
    /// Count Expect Commands
    /// </summary>
    public class Count
    {
        private AutomationProvider _automation = null;
        private int _count = int.MinValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="Count"/> class.
        /// </summary>
        /// <param name="automation">The automation.</param>
        /// <param name="count">The count.</param>
        public Count(AutomationProvider automation, int count)
        {
            _automation = automation;
            _count = count;
        }

        /// <summary>
        /// Expects the specified selector matches a specified number of fields.
        /// </summary>
        /// <param name="fieldSelector">The field selector.</param>
        public void Of(string fieldSelector)
        {
            Of(fieldSelector, MatchConditions.None);
        }

        /// <summary>
        /// Expects the specified selector and conditions match a specified number of fields.
        /// </summary>
        /// <param name="fieldSelector">The field selector.</param>
        /// <param name="conditions">The conditions.</param>
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
