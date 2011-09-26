// <copyright file="SelectElement.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;
using System.Linq;
using System.Linq.Expressions;
using FluentAutomation.API;
using FluentAutomation.API.Enumerations;
using FluentAutomation.API.Exceptions;
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

        public override string GetText()
        {
            return _element.SelectedOption.Text;
        }

        public string GetSelectedOptionText()
        {
            return _element.SelectedOption.Text;
        }

        public string[] GetValues()
        {
            return _element.AllSelectedOptions.Select(o => o.GetAttribute("value") ?? o.Text).ToArray();
        }

        public string[] GetOptionValues()
        {
            return _element.Options.Select(o => o.GetAttribute("value") ?? o.Text).ToArray();
        }

        public string[] GetOptionsText()
        {
            return _element.Options.Select(o => o.Text).ToArray();
        }

        public int GetSelectedIndex()
        {
            return _element.Options.IndexOf(_element.SelectedOption);
        }

        public int[] GetSelectedIndices()
        {
            return _element.AllSelectedOptions.Select(o => _element.Options.IndexOf(o)).ToArray();
        }

        public void SetValue(string value, SelectMode selectMode)
        {
            if (selectMode == SelectMode.Value)
            {
                try
                {
                    _element.SelectByValue(value);
                }
                catch (NoSuchElementException)
                {
                }
            }
            else if (selectMode == SelectMode.Text)
            {
                try
                {
                    _element.SelectByText(value);
                }
                catch (NoSuchElementException)
                {
                    throw new NoSuchElementException("Cannot locate option with text: " + value);
                }
            }
            else if (selectMode == SelectMode.Index)
            {
                try
                {
                    _element.SelectByIndex(Int32.Parse(value));
                }
                catch (NoSuchElementException)
                {
                    throw new NoSuchElementException("Cannot location option at index: " + value);
                }
            }

            this.OnChange();
        }

        public override void SetValue(string value)
        {
            SetValue(value, SelectMode.Value);
        }

        public void SetValues(string[] values, SelectMode selectMode)
        {
            foreach (var value in values)
            {
                SetValue(value, selectMode);
            }

            if (_element.AllSelectedOptions.Count == 0)
            {
                if (selectMode == SelectMode.Value)
                    throw new SelectException("Selection failed. No option values matched collection provided.");
                else if (selectMode == SelectMode.Text)
                    throw new SelectException("Selection failed. No options text matched collection provided.");
            }
        }

        public void SetValues(Expression<Func<string, bool>> optionMatchingExpression, SelectMode selectMode)
        {
            var compiledFunc = optionMatchingExpression.Compile();
            if (selectMode == SelectMode.Value)
            {
                var options = _element.Options.Where(x => compiledFunc(x.GetAttribute("value")));
                foreach (var option in options)
                {
                    _element.SelectByValue(option.GetAttribute("value"));
                }

                if (options.Count() == 0)
                {
                    throw new SelectException("Selection failed. No option values matched expression [{0}] on element.", optionMatchingExpression.ToExpressionString());
                }
            }
            else if (selectMode == SelectMode.Text)
            {
                var options = _element.Options.Where(x => compiledFunc(x.Text));
                foreach (var option in options)
                {
                    _element.SelectByText(option.Text);
                }

                if (options.Count() == 0)
                {
                    throw new SelectException("Selection failed. No option text matched expression [{0}] on element.", optionMatchingExpression.ToExpressionString());
                }
            }

            this.OnChange();
        }

        public void SetSelectedIndex(int selectedIndex)
        {
            SetValue(selectedIndex.ToString(), SelectMode.Index);
        }

        public void SetSelectedIndices(int[] selectedIndices)
        {
            foreach (var selectedIndex in selectedIndices)
            {
                SetValue(selectedIndex.ToString(), SelectMode.Index);
            }

            if (_element.AllSelectedOptions.Count == 0)
            {
                throw new SelectException("Selection failed. No options matched collection of indices provided.");
            }
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
