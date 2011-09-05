using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using FluentAutomation.API.Interfaces;

namespace FluentAutomation.SeleniumWebDriver
{
    public class TextElement : Element, ITextElement
    {
        private IWebElement _element = null;

        public TextElement(IWebDriver driver, IWebElement element, string fieldSelector)
            : base(driver, element, fieldSelector)
        {
            _element = element;
        }

        public void SetValueQuickly(string value)
        {
            SetValue(value);
        }

        public override string GetValue()
        {
            return _element.Text;
        }

        public override void SetValue(string value)
        {
            _element.Clear();
            _element.SendKeys(value);
            _element.SendKeys(Keys.Tab);
        }
    }
}
