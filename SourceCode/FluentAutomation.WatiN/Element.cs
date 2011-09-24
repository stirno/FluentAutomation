// <copyright file="Element.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FluentAutomation.API.Interfaces;
using Automation = global::WatiN;
using FluentAutomation.API;

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
            if (_element.TagName.Equals("input", StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetAttributeValue("value");
            }
            else
            {
                return _element.Text;
            }
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

        public void DragTo(IElement dropElement)
        {
            var dragPoint = this.GetElementBounds();
            MouseControl.SetPosition(new API.Point(dragPoint.X, dragPoint.Y));
            MouseControl.MouseEvent(MouseControl.MouseEvent_LeftButtonDown, dragPoint.X, dragPoint.Y, 0, 0);

            var dropPoint = dropElement.GetElementBounds();
            MouseControl.SetPosition(new API.Point(dropPoint.X, dropPoint.Y));
            MouseControl.MouseEvent(MouseControl.MouseEvent_LeftButtonUp, dropPoint.X, dropPoint.Y, 0, 0);
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
            _element.ClickNoWait();
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
