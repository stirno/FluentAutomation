// <copyright file="TextElement.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using FluentAutomation.API.Interfaces;
using OpenQA.Selenium;

namespace FluentAutomation.SeleniumWebDriver
{
    public class CheckBoxElement : Element, ICheckBoxElement
    {
    	private IWebElement _element = null;

		public CheckBoxElement(IWebDriver driver, IWebElement element, string fieldSelector)
            : base(driver, element, fieldSelector)
		{
			_element = element;
		}

    	public bool Checked
    	{
    		get { return _element.Selected; }
    		set
    		{
    			if (value != _element.Selected)
    			{
    				_element.Click();
    			}
    		}
    	}
    }
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
            return string.IsNullOrEmpty(_element.Text) ? _element.GetAttribute("value") : _element.Text;
        }

        public override void SetValue(string value)
        {
            _element.Clear();
            _element.SendKeys(value);
            this.OnChange();
        }
    }
}
