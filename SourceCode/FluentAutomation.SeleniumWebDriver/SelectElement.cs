using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Interfaces;
using OpenQA.Selenium;

namespace FluentAutomation.SeleniumWebDriver
{
    class SelectElement : Element, ISelectElement
    {
        private OpenQA.Selenium.Support.UI.SelectElement _element = null;

        public SelectElement(IWebDriver driver, IWebElement element, string fieldSelector)
            : base(driver, element, fieldSelector)
        {
            _element = new OpenQA.Selenium.Support.UI.SelectElement(element);
        }

        public bool IsMultiple
        {
            get
            {
                return _element.IsMultiple;
            }
        }

        public override string GetValue()
        {
            return _element.SelectedOption.GetAttribute("value") ?? _element.SelectedOption.Text;
        }

        public string[] GetValues()
        {
            return _element.AllSelectedOptions.Select(o => o.GetAttribute("value") ?? o.Text).ToArray();
        }

        public string[] GetOptionValues()
        {
            return _element.Options.Select(o => o.GetAttribute("value") ?? o.Text).ToArray();
        }

        public int GetSelectedIndex()
        {
            return _element.Options.IndexOf(_element.SelectedOption);
        }

        public int[] GetSelectedIndices()
        {
            return _element.AllSelectedOptions.Select(o => _element.Options.IndexOf(o)).ToArray();
        }

        public override void SetValue(string value)
        {
            try
            {
                _element.SelectByValue(value);
            }
            catch (NoSuchElementException)
            {
                try
                {
                    _element.SelectByText(value);
                }
                catch (NoSuchElementException)
                {
                    throw new NoSuchElementException("Cannot locate option with value or text: " + value);
                }
            }

            this.OnChange();
        }

        public void SetValues(string[] values)
        {
            foreach (var value in values)
            {
                try
                {
                    _element.SelectByValue(value);
                }
                catch (NoSuchElementException)
                {
                    try
                    {
                        _element.SelectByText(value);
                    }
                    catch (NoSuchElementException)
                    {
                        throw new NoSuchElementException("Cannot locate option with value or text: " + value);
                    }
                }
            }

            this.OnChange();
        }

        public void SetSelectedIndex(int selectedIndex)
        {
            _element.SelectByIndex(selectedIndex);
            this.OnChange();
        }

        public void SetSelectedIndices(int[] selectedIndices)
        {
            foreach (var selectedIndex in selectedIndices)
            {
                _element.SelectByIndex(selectedIndex);
            }

            this.OnChange();
        }

        public void ClearSelectedItems()
        {
            if (_element.IsMultiple)
            {
                try
                {
                    _element.DeselectAll();
                }
                catch (StaleElementReferenceException) { }
            }
        }
    }
}
