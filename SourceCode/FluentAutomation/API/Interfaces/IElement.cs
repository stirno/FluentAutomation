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
    public interface IElement
    {
        /// <summary>
        /// Gets the attribute value.
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns></returns>
        string GetAttributeValue(string attributeName);

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <returns></returns>
        string GetText();

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns></returns>
        string GetValue();

        /// <summary>
        /// Gets the element bounds.
        /// </summary>
        /// <returns></returns>
        Rectangle GetElementBounds();

        /// <summary>
        /// Determines whether this instance is select.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is select; otherwise, <c>false</c>.
        /// </returns>
        bool IsSelect();

        /// <summary>
        /// Determines whether this instance is text.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is text; otherwise, <c>false</c>.
        /// </returns>
        bool IsText();

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
