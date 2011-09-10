// <copyright file="Element.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Interfaces;
using OpenQA.Selenium;
using System.Drawing;

namespace FluentAutomation.SeleniumWebDriver
{
    public class Element : IElement
    {
        private IWebDriver _driver = null;
        private IWebElement _element = null;
        private string _fieldSelector = string.Empty;

        public Element(IWebDriver driver, IWebElement element, string fieldSelector)
        {
            _driver = driver;
            _element = element;
            _fieldSelector = fieldSelector;
        }

        public string GetAttributeValue(string attributeName)
        {
            return _element.GetAttribute(attributeName);
        }

        public virtual string GetText()
        {
            return _element.Text;
        }

        public virtual string GetValue()
        {
            if (_element.TagName.Equals("input", StringComparison.InvariantCultureIgnoreCase))
            {
                return _element.GetAttribute("value");
            }
            else
            {
                return _element.Text;
            }
        }

        public System.Drawing.Rectangle GetElementBounds()
        {
            return new Rectangle(_element.Location, _element.Size);
        }

        public IWebElement GetWebElement()
        {
            return _element;
        }

        public void DragTo(IElement element)
        {
            (new OpenQA.Selenium.Interactions.Actions(_driver)).DragAndDrop(_element, ((Element)element).GetWebElement()).Perform();
        }

        public bool IsSelect()
        {
            return _element.TagName.Equals("select", StringComparison.InvariantCultureIgnoreCase);
        }

        public bool IsText()
        {
            return (_element.TagName.Equals("input", StringComparison.InvariantCultureIgnoreCase) &&
                (_element.GetAttribute("type").Equals("text", StringComparison.InvariantCultureIgnoreCase) ||
                _element.GetAttribute("type").Equals("password", StringComparison.InvariantCultureIgnoreCase) ||
                _element.GetAttribute("type").Equals("hidden", StringComparison.InvariantCultureIgnoreCase)) ||
                _element.TagName.Equals("textarea", StringComparison.InvariantCultureIgnoreCase));
        }

        public virtual void SetValue(string value)
        {
            throw new NotImplementedException("Cannot set value to a generic Element.");
        }

        public virtual void Click()
        {
            _element.Click();
        }

        public virtual void Focus()
        {
            
        }

        public virtual void Hover()
        {
            FluentAutomation.API.MouseControl.SetPosition(new API.Point(_element.Location.X, _element.Location.Y));
        }

        public virtual void OnChange()
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript(string.Format("if (typeof jQuery != 'undefined') {{ jQuery(\"{0}\").change(); }}", _fieldSelector.Replace("\"", "")));
        }
    }
}
