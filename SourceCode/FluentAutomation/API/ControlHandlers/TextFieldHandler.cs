using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;

namespace FluentAutomation.API.ControlHandlers
{
    public class TextFieldHandler
    {
        private Browser _browser = null;
        private string _value = string.Empty;
        private bool _quickEnter = false;
        private TextFieldHandler _quicklyHandler = null;

        public TextFieldHandler(Browser browser, string value)
        {
            _browser = browser;
            _value = value;
        }

        private TextFieldHandler(Browser browser, string value, bool quickEnter) : this(browser, value)
        {
            _quickEnter = quickEnter;
        }

        public TextFieldHandler Quickly
        {
            get
            {
                if (_quicklyHandler == null)
                {
                    _quicklyHandler = new TextFieldHandler(_browser, _value, true);
                }

                return _quicklyHandler;
            }
        }

        public void In(string fieldSelector)
        {
            TextField element = _browser.ElementOfType<TextField>(Find.BySelector(fieldSelector));

            if (_quickEnter)
            {
                element.Value = _value;
            }
            else
            {
                element.TypeText(_value);
            }

            element.FireJavaScriptChange();
        }

        public void In(Func<string, string> fieldSelectorFunc)
        {
            In(fieldSelectorFunc(_value));
        }

        public void In(Func<string, bool> fieldSelectorFunc)
        {
            string fieldSelector = string.Empty;
            foreach (var child in _browser.ElementsOfType<TextField>())
            {
                bool isMatch = fieldSelectorFunc(child.ClassName);
                if (!isMatch) isMatch = fieldSelectorFunc(child.Id);
                if (!isMatch) isMatch = fieldSelectorFunc(child.Name);

                if (isMatch) fieldSelector = child.IdOrName;
            }

            if (fieldSelector != string.Empty)
            {
                In(fieldSelector);
            }
        }
    }
}
