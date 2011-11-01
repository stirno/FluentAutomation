using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.API.Interfaces
{
    /// <summary>
    /// Interface for use in Element Funcs
    /// </summary>
    public interface IElementDetails
    {
        /// <summary>
        /// Gets the position.
        /// </summary>
        API.Point Position { get; }

        /// <summary>
        /// Gets the size.
        /// </summary>
        API.Size Size { get; }

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
        /// Determines whether this instance is text.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is text; otherwise, <c>false</c>.
        /// </returns>
        bool IsCheckBox();
    }
}
