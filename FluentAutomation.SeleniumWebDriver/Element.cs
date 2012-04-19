using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAutomation.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FluentAutomation
{
    public class Element : IElement
    {
        private string selector = null;

        public Element(IWebElement webElement)
        {
            this.WebElement = webElement;
        }

        public Element(IWebElement webElement, string selector)
        {
            this.WebElement = webElement;
            this.selector = selector;
        }

        public string TagName
        {
            get
            {
                return this.WebElement.TagName.ToLower();
            }
        }

        public string Value
        {
            get
            {
                if (this.TagName == "input")
                {
                    return this.Attributes.Get("value");
                }
                else
                {
                    return this.Text;
                }
            }
        }

        public string Text
        {
            get
            {
                return this.WebElement.Text;
            }
        }

        public string Selector
        {
            get
            {
                return this.selector;
            }
        }

        public IEnumerable<string> SelectedOptionValues
        {
            get
            {
                if (this.IsSelect)
                {
                    SelectElement selectElement = new SelectElement(this.WebElement);
                    return selectElement.AllSelectedOptions.Select(x => x.GetAttribute("value"));
                }
                else
                {
                    return null;
                }
            }
        }

        public IEnumerable<string> SelectedOptionTextCollection
        {
            get
            {
                if (this.IsSelect)
                {
                    SelectElement selectElement = new SelectElement(this.WebElement);
                    return selectElement.AllSelectedOptions.Select(x => x.Text);
                }
                else
                {
                    return null;
                }
            }
        }

        public bool IsText
        {
            get
            {
                bool isText = false;
                switch (this.TagName)
                {
                    case "input":
                        switch (this.Attributes.Get("type").ToLower())
                        {
                            case "text":
                            case "password":
                            case "hidden":
                                isText = true;
                                break;
                        }

                        break;
                    case "textarea":
                        isText = true;
                        break;
                }

                return isText;
            }
        }

        public bool IsSelect
        {
            get
            {
                return this.WebElement.TagName.ToLower() == "select";
            }
        }

        public bool IsMultipleSelect
        {
            get
            {
                if (this.IsSelect)
                {
                    SelectElement selectElement = new SelectElement(this.WebElement);
                    return selectElement.IsMultiple;
                }
                else
                {
                    return false;
                }
            }
        }

        private IElementAttributeSelector attributes = null;
        public IElementAttributeSelector Attributes
        {
            get
            {
                if (attributes == null)
                {
                    attributes = new ElementAttributeSelector(this.WebElement);
                }

                return attributes;
            }
        }

        public IWebElement WebElement { get; set; }
    }

    public class ElementAttributeSelector : IElementAttributeSelector
    {
        private readonly IWebElement webElement = null;

        public ElementAttributeSelector(IWebElement webElement)
        {
            this.webElement = webElement;
        }

        public string Get(string name)
        {
            var attributeValue = webElement.GetAttribute(name);
            return attributeValue;
        }
    }
}
