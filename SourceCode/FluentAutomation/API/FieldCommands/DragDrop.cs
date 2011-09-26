// <copyright file="DragDrop.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using FluentAutomation.API.Enumerations;
using FluentAutomation.API.Providers;

namespace FluentAutomation.API.FieldCommands
{
    /// <summary>
    /// DragDrop Commands
    /// </summary>
    public class DragDrop
    {
        private AutomationProvider _automation = null;
        private string _dragFieldSelector = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="DragDrop"/> class.
        /// </summary>
        /// <param name="automation">The automation.</param>
        /// <param name="fieldSelector">The field selector.</param>
        internal DragDrop(AutomationProvider automation, string fieldSelector)
        {
            _automation = automation;
            _dragFieldSelector = fieldSelector;
        }

        /// <summary>
        /// Drops the element on the specified field.
        /// </summary>
        /// <param name="fieldSelector">The field selector.</param>
        public void To(string fieldSelector)
        {
            var element = _automation.GetElement(_dragFieldSelector, MatchConditions.None);
            element.DragTo(_automation.GetElement(fieldSelector, MatchConditions.None));
        }
    }
}
