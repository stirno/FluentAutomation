using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class Element : IElement
    {
        public Element(string selector)
        {
            this.selector = selector;
        }

        private string selector = null;
        public string Selector
        {
            get { return this.selector; }
        }

        public string TagName
        {
            get { return string.Empty; }
        }

        public IElementAttributeSelector Attributes
        {
            get { return new ElementAttributeSelector(); }
        }

        public string Value
        {
            get { return string.Empty; }
        }

        public string Text
        {
            get { return string.Empty; }
        }

        public IEnumerable<string> SelectedOptionValues
        {
            get { return new List<string>() { }; }
        }

        public IEnumerable<string> SelectedOptionTextCollection
        {
            get { return new List<string>() { }; }
        }

        public bool IsText
        {
            get { return true; }
        }

        public bool IsSelect
        {
            get { return false; }
        }

        public bool IsMultipleSelect
        {
            get { return false; }
        }
    }

    public class ElementAttributeSelector : IElementAttributeSelector
    {
        public ElementAttributeSelector()
        {
        }

        public string Get(string name)
        {
            return name;
        }
    }
}
