using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FluentAutomation.API.Interfaces;
using Automation = global::WatiN;

namespace FluentAutomation.WatiN
{
    public class Element : IElement
    {
        private Automation.Core.Element _element = null;

        public Element(Automation.Core.Element element)
        {
            _element = element;
        }

        public virtual string GetValue()
        {
            return _element.Text;
        }

        public virtual void SetValue(string value)
        {
            throw new NotImplementedException("Cannot set value to a generic Element.");
        }

        public string GetAttributeValue(string attributeName)
        {
            // WatiN hides class attribute, so handle that scenario
            if (attributeName.Equals("class", StringComparison.InvariantCultureIgnoreCase))
            {
                return _element.ClassName;
            }

            return _element.GetAttributeValue(attributeName);
        }
        
        public virtual string GetText()
        {
            return _element.Text;
        }

        public Rectangle GetElementBounds()
        {
            return _element.NativeElement.GetElementBounds();
        }

        public bool IsSelect()
        {
            return _element.TagName.Equals("select", StringComparison.InvariantCultureIgnoreCase);
        }

        public bool IsText()
        {
            return (_element.TagName.Equals("input", StringComparison.InvariantCultureIgnoreCase) &&
                (_element.GetAttributeValue("type").Equals("text", StringComparison.InvariantCultureIgnoreCase) ||
                _element.GetAttributeValue("type").Equals("password", StringComparison.InvariantCultureIgnoreCase) ||
                _element.GetAttributeValue("type").Equals("hidden", StringComparison.InvariantCultureIgnoreCase)) ||
                _element.TagName.Equals("textarea", StringComparison.InvariantCultureIgnoreCase));
        }

        public virtual void Click()
        {
            _element.Click();
        }

        public virtual void Focus()
        {
            _element.Focus();
        }

        public virtual void Hover()
        {
            _element.MouseEnter();
        }

        public virtual void OnChange()
        {
            _element.DomContainer.Eval(string.Format("if (typeof jQuery != 'undefined') {{ jQuery({0}).change(); }}", _element.GetJavascriptElementReference()));
        }
    }
}
