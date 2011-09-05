using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Enumerations;

namespace FluentAutomation.API.Interfaces
{
    public interface ISelectElement : IElement
    {
        string[] GetValues();
        int GetSelectedIndex();
        int[] GetSelectedIndices();
        string[] GetOptionValues();
        void SetValues(string[] values);
        void SetSelectedIndex(int selectedIndex);
        void SetSelectedIndices(int[] selectedIndices);
        void ClearSelectedItems();
        bool IsMultiple { get; }
    }
}
