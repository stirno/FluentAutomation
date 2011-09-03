using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;

namespace FluentAutomation.API.ControlHandlers
{
    public class SelectListHandler
    {
        private Browser _browser = null;
        private SelectActionType _actionType = SelectActionType.SingleByValue;

        private int _selectedIndex = -1;
        private IEnumerable<int> _selectedIndices = null;
        private string _selectedValue = string.Empty;
        private IEnumerable<string> _selectedValues = null;

        private enum SelectActionType
        {
            SingleByIndex = 1,
            SingleByValue = 2,
            MultipleByIndex = 3,
            MultipleByValue = 4
        }

        private SelectListHandler(Browser browser, SelectActionType actionType)
        {
            _browser = browser;
            _actionType = actionType;
        }

        public SelectListHandler(Browser browser, int selectedIndex) : this(browser, SelectActionType.SingleByIndex)
        {
            _selectedIndex = selectedIndex;
        }

        public SelectListHandler(Browser browser, IEnumerable<int> selectedIndices) : this(browser, SelectActionType.MultipleByIndex)
        {
            _selectedIndices = selectedIndices;
        }

        public SelectListHandler(Browser browser, string selectedValue) : this(browser, SelectActionType.SingleByValue)
        {
            _selectedValue = selectedValue;
        }

        public SelectListHandler(Browser browser, IEnumerable<string> selectedValues) : this(browser, SelectActionType.MultipleByValue)
        {
            _selectedValues = selectedValues;
        }

        public void From(string fieldSelector)
        {
            SelectList element = _browser.ElementOfType<SelectList>(Find.BySelector(fieldSelector));

            if (_actionType == SelectActionType.SingleByValue)
            {
                element.Select(_selectedValue);
            }
            else if (_actionType == SelectActionType.MultipleByValue)
            {
                if (element.Multiple)
                {
                    element.ClearList();
                    foreach (var value in _selectedValues)
                    {
                        element.Select(value);
                    }
                }
            }
            else if (_actionType == SelectActionType.SingleByIndex)
            {
                element.Options[_selectedIndex].Select();
            }
            else if (_actionType == SelectActionType.MultipleByIndex)
            {
                if (element.Multiple)
                {
                    element.ClearList();
                    foreach (var index in _selectedIndices)
                    {
                        element.Options[index].Select();
                    }
                }
            }

            element.FireJavaScriptChange();
        }

        public void From(Func<string, string> fieldSelectorFunc)
        {
            fieldSelectorFunc(_selectedValue);
        }

        public void From(Func<IEnumerable<string>, string> fieldSelectorFunc)
        {
            fieldSelectorFunc(_selectedValues);
        }

        public void From(Func<int, string> fieldSelectorFunc)
        {
            fieldSelectorFunc(_selectedIndex);
        }

        public void From(Func<IEnumerable<int>, string> fieldSelectorFunc)
        {
            fieldSelectorFunc(_selectedIndices);
        }
    }
}
