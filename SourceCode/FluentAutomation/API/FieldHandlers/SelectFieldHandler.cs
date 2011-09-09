// <copyright file="SelectFieldHandler.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System.Linq;
using FluentAutomation.API.Providers;

namespace FluentAutomation.API.FieldHandlers
{
    public class SelectFieldHandler
    {
        private AutomationProvider _automation = null;
        private string[] _values = null;
        private int[] _selectedIndices = null;

        public SelectFieldHandler(AutomationProvider automationProvider, string[] values)
        {
            _automation = automationProvider;
            _values = values;
        }

        public SelectFieldHandler(AutomationProvider automationProvider, int[] selectedIndices)
        {
            _automation = automationProvider;
            _selectedIndices = selectedIndices;
        }

        public void From(string fieldSelector)
        {
            var field = _automation.GetSelectElement(fieldSelector);
            field.ClearSelectedItems();

            if (_selectedIndices == null)
            {
                if (_values.Length == 1)
                {
                    field.SetValue(_values.First());
                }
                else if (_values.Length > 1)
                {
                    field.SetValues(_values);
                }
            }
            else
            {
                if (_selectedIndices.Length == 1)
                {
                    field.SetSelectedIndex(_selectedIndices.First());
                }
                else if (_selectedIndices.Length > 1)
                {
                    field.SetSelectedIndices(_selectedIndices);
                }
            }
        }
    }
}
