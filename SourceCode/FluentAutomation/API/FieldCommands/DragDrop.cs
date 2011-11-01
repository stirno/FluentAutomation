// <copyright file="DragDrop.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using FluentAutomation.API.Enumerations;
using FluentAutomation.API.Providers;
using System.Collections.Generic;

namespace FluentAutomation.API.FieldCommands
{
    /// <summary>
    /// DragDrop Commands
    /// </summary>
    public class DragDrop : CommandBase
    {
        private string _dragFieldSelector = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="DragDrop"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="fieldSelector">The field selector.</param>
        public DragDrop(AutomationProvider provider, CommandManager manager, string fieldSelector) : base(provider, manager)
        {
            _dragFieldSelector = fieldSelector;
        }

        /// <summary>
        /// Drops the element on the specified field.
        /// </summary>
        /// <param name="fieldSelector">The field selector.</param>
        public void To(string fieldSelector)
        {
            if (CommandManager.EnableRemoteExecution)
            {
                CommandManager.RemoteCommands.Add(new RemoteCommands.RemoteCommandDetails()
                {
                    Name = "Drag",
                    Arguments = new Dictionary<string, dynamic>()
                    {
                        { "from", _dragFieldSelector },
                        { "to", fieldSelector }
                    }
                });
            }
            else
            {
                CommandManager.CurrentActionBucket.Add(() =>
                {
                    var element = Provider.GetElement(_dragFieldSelector, MatchConditions.None);
                    element.DragTo(Provider.GetElement(fieldSelector, MatchConditions.None));
                });
            }
        }
    }
}
