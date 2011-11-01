// <copyright file="ISelectElement.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using FluentAutomation.API.Enumerations;
using System;
using System.Linq.Expressions;

namespace FluentAutomation.API.Interfaces
{
	/// <summary>
    /// Interface for <select />
    /// </summary>
    public interface ISelectElement : IElement
    {
        /// <summary>
        /// Gets the selected option text.
        /// </summary>
        /// <returns></returns>
        string GetSelectedOptionText();

        /// <summary>
        /// Gets the selected options text.
        /// </summary>
        /// <returns></returns>
        string[] GetSelectedOptionsText();

        /// <summary>
        /// Gets the selected values.
        /// </summary>
        /// <returns></returns>
        string[] GetValues();

        /// <summary>
        /// Gets the selected option index.
        /// </summary>
        /// <returns></returns>
        int GetSelectedIndex();

        /// <summary>
        /// Gets the selected option indices.
        /// </summary>
        /// <returns></returns>
        int[] GetSelectedIndices();

        /// <summary>
        /// Gets the option values.
        /// </summary>
        /// <returns></returns>
        string[] GetOptionValues();

        /// <summary>
        /// Gets the options text.
        /// </summary>
        /// <returns></returns>
        string[] GetOptionsText();

        /// <summary>
        /// Sets the values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="selectMode">The select mode.</param>
        void SetValues(string[] values, SelectMode selectMode);

        /// <summary>
        /// Sets the values.
        /// </summary>
        /// <param name="matchingFunc">The matching func.</param>
        /// <param name="selectMode">The select mode.</param>
        void SetValues(Expression<Func<string, bool>> matchingFunc, SelectMode selectMode);

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="selectMode">The select mode.</param>
        void SetValue(string value, SelectMode selectMode);

        /// <summary>
        /// Sets the index of the selected.
        /// </summary>
        /// <param name="selectedIndex">Index of the selected.</param>
        void SetSelectedIndex(int selectedIndex);

        /// <summary>
        /// Sets the selected indices.
        /// </summary>
        /// <param name="selectedIndices">The selected indices.</param>
        void SetSelectedIndices(int[] selectedIndices);

        /// <summary>
        /// Clears the selected items.
        /// </summary>
        void ClearSelectedItems();

        /// <summary>
        /// Gets a value indicating whether this instance is multiple.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is multiple; otherwise, <c>false</c>.
        /// </value>
        bool IsMultiple { get; }
    }
}
