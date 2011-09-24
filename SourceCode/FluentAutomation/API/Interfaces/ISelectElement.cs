// <copyright file="ISelectElement.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using FluentAutomation.API.Enumerations;
using System;

namespace FluentAutomation.API.Interfaces
{
    public interface ISelectElement : IElement
    {
        string GetOptionText();
        string[] GetValues();
        int GetSelectedIndex();
        int[] GetSelectedIndices();
        string[] GetOptionValues();
        string[] GetOptionsText();
        void SetValues(string[] values, SelectMode selectMode);
        void SetValues(Func<string, bool> matchingFunc, SelectMode selectMode);
        void SetValue(string value, SelectMode selectMode);
        void SetSelectedIndex(int selectedIndex);
        void SetSelectedIndices(int[] selectedIndices);
        void ClearSelectedItems();
        bool IsMultiple { get; }
    }
}
