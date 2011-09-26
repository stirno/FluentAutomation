// <copyright file="ITextElement.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

namespace FluentAutomation.API.Interfaces
{
    /// <summary>
    /// Interface for <input type="text" /> and <textarea />
    /// </summary>
    public interface ITextElement : IElement
    {
        /// <summary>
        /// Sets the value without firing KeyUp, KeyDown events.
        /// </summary>
        /// <param name="value">The value.</param>
        void SetValueQuickly(string value);
    }
}
