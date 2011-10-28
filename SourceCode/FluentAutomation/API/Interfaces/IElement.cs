// <copyright file="IElement.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System.Drawing;
using FluentAutomation.API.Enumerations;

namespace FluentAutomation.API.Interfaces
{
    /// <summary>
    /// Interface for any DOM element
    /// </summary>
    public interface IElement : IElementDetails
    {
        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        void SetValue(string value);

        /// <summary>
        /// Focuses this instance.
        /// </summary>
        void Click(ClickMode clickMode);

        /// <summary>
        /// Focuses this instance.
        /// </summary>
        void Focus();

        /// <summary>
        /// Hovers this instance.
        /// </summary>
        void Hover();

        /// <summary>
        /// Drags to.
        /// </summary>
        /// <param name="fieldSelector">The field selector.</param>
        void DragTo(IElement fieldSelector);
    }
}
