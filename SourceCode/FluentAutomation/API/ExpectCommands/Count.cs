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
    public class Count : CommandBase
    {
        private int _count = int.MinValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="Count"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="count">The count.</param>
        public Count(AutomationProvider provider, CommandManager manager, int count) : base(provider, manager)
        {
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
            CommandManager.CurrentActionBucket.Add(() =>
            {
                var elements = Provider.GetElements(fieldSelector, conditions);

                if (elements.Count() != _count)
                {
                	Provider.TakeAssertExceptionScreenshot();
                    throw new AssertException("Count assertion failed. Expected there to be [{0}] elements matching [{1}]. Actual count is [{2}]", _count, fieldSelector, elements.Count());
                }
            });
        }
    }
}
