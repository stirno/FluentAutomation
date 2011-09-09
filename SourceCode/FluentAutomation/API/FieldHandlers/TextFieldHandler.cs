// <copyright file="TextFieldHandler.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;
using FluentAutomation.API.Providers;

namespace FluentAutomation.API.FieldHandlers
{
    public class TextFieldHandler
    {
        private AutomationProvider _automation = null;
        private string _value = string.Empty;
        private bool _quickEntry = false;

        public TextFieldHandler(AutomationProvider automationProvider, string value)
        {
            _automation = automationProvider;
            _value = value;
        }

        public TextFieldHandler Quickly
        {
            get
            {
                _quickEntry = true;
                return this;
            }
        }

        public void In(string fieldSelector)
        {
            var field = _automation.GetTextElement(fieldSelector);

            if (_quickEntry)
            {
                field.SetValueQuickly(_value);
            }
            else
            {
                field.SetValue(_value);
            }
        }

        public void In(Func<string, string> fieldSelectorFunc)
        {
            In(fieldSelectorFunc(_value));
        }
    }
}
