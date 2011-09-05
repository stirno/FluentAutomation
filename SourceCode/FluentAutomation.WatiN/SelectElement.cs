using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Interfaces;
using Automation = global::WatiN;

namespace FluentAutomation.WatiN
{
    class SelectElement : Element, ISelectElement
    {
        private Automation.Core.SelectList _element = null;

        public SelectElement(Automation.Core.SelectList element) : base(element)
        {
            _element = element;
        }

        public bool IsMultiple
        {
            get
            {
                return _element.Multiple;
            }
        }

        public override string GetValue()
        {
            return _element.SelectedOption.Value;
        }

        public string[] GetValues()
        {
            return _element.Options.Where(o => o.Selected).Select(o => o.Value).ToArray();
        }

        public string[] GetOptionValues()
        {
            return _element.Options.Select(o => o.Value).ToArray();
        }

        public int GetSelectedIndex()
        {
            return _element.SelectedOption.Index;
        }

        public int[] GetSelectedIndices()
        {
            return _element.Options.Where(o => o.Selected).Select(o => o.Index).ToArray();
        }

        public override void SetValue(string value)
        {
            _element.Select(value);
        }

        public void SetValues(string[] values)
        {
            foreach (var value in values)
            {
                _element.Select(value);
            }
        }

        public void SetSelectedIndex(int selectedIndex)
        {
            _element.Options[selectedIndex].Select();
        }

        public void SetSelectedIndices(int[] selectedIndices)
        {
            foreach (var selectedIndex in selectedIndices)
            {
                _element.Options[selectedIndex].Select();
            }
        }

        public void ClearSelectedItems()
        {
            _element.ClearList();
        }
    }
}
