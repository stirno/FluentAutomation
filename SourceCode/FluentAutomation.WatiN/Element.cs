// <copyright file="Element.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;
using System.Drawing;
using FluentAutomation.API;
using FluentAutomation.API.Interfaces;
using Automation = global::WatiN;
using FluentAutomation.API.Enumerations;

namespace FluentAutomation.WatiN
{
    public class Element : IElement
    {
        private Automation.Core.Element _element = null;

        public Element(Automation.Core.Element element)
        {
            _element = element;
        }

        public API.Point Position
        {
            get
            {
                var bounds = this._element.NativeElement.GetElementBounds();
                return new API.Point(bounds.Left, bounds.Top);
            }
        }

        public API.Size Size
        {
            get
            {
                var bounds = this._element.NativeElement.GetElementBounds();
                return new API.Size(bounds.Size.Width, bounds.Size.Height);
            }
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

        public void DragTo(IElement dropElement)
        {
            MouseControl.SetPosition(this.Position);
            MouseControl.MouseEvent(MouseControl.MouseEvent_LeftButtonDown, this.Position.X, this.Position.Y, 0, 0);

            MouseControl.SetPosition(dropElement.Position);
            MouseControl.MouseEvent(MouseControl.MouseEvent_LeftButtonUp, dropElement.Position.X, dropElement.Position.Y, 0, 0);
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

		public bool IsCheckBox()
        {
            return (_element.TagName.Equals("input", StringComparison.InvariantCultureIgnoreCase) &&
                (_element.GetAttributeValue("type").Equals("checkbox", StringComparison.InvariantCultureIgnoreCase)));
        }

        public virtual void Click()
        {
            Click(ClickMode.Default);
        }

        public virtual void Click(ClickMode clickMode)
        {
            if (clickMode == ClickMode.NoWait)
            {
                _element.ClickNoWait();
            }
            else
            {
                _element.Click();
            }
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
