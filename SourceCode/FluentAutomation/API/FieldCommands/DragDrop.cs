// <copyright file="DraggedFieldHandler.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using FluentAutomation.API.Providers;
using FluentAutomation.API.Enumerations;

namespace FluentAutomation.API.FieldCommands
{
    public class DragDrop
    {
        private AutomationProvider _automation = null;
        private string _dragFieldSelector = string.Empty;

        internal DragDrop(AutomationProvider automation, string fieldSelector)
        {
            _automation = automation;
            _dragFieldSelector = fieldSelector;
        }

        public void To(string fieldSelector)
        {
            var element = _automation.GetElement(_dragFieldSelector, MatchConditions.None);
            element.DragTo(_automation.GetElement(fieldSelector, MatchConditions.None));
        }
    }
}
