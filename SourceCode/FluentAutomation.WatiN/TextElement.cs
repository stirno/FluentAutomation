﻿// <copyright file="TextElement.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using FluentAutomation.API.Interfaces;
using Automation = global::WatiN;

namespace FluentAutomation.WatiN
{
	public class TextElement : Element, ITextElement
    {
        private Automation.Core.TextField _element = null;

        public TextElement(Automation.Core.TextField element) : base(element)
        {
            _element = element;
        }

        public void SetValueQuickly(string value)
        {
            _element.Value = value;
            this.OnChange();
        }

        public override string GetValue()
        {
            return _element.Value;
        }

        public override void SetValue(string value)
        {
            _element.TypeText(value);
            this.OnChange();
        }
    }
}
