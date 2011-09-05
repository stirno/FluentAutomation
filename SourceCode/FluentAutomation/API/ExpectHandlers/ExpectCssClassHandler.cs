using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAutomation.API.Providers;

namespace FluentAutomation.API.ExpectHandlers
{
    public class ExpectCssClassHandler
    {
        private AutomationProvider _automation = null;
        private string _value = string.Empty;

        public ExpectCssClassHandler(AutomationProvider automation, string value)
        {
            _automation = automation;
            _value = value;
        }

        public void On(string fieldSelector)
        {
            var element = _automation.GetElement(fieldSelector);
            string className = _value.Replace(".", "").Trim();
            string elementClassName = element.GetAttributeValue("class").Trim();

            if (elementClassName.Contains(' '))
            {
                string[] classes = elementClassName.Split(' ');
                bool hasMatches = false;
                foreach (var cssClass in classes)
                {
                    var cssClassString = cssClass.Trim();
                    if (!string.IsNullOrEmpty(cssClassString))
                    {
                        if (cssClassString.Equals(className))
                        {
                            hasMatches = true;
                        }
                    }
                }

                if (!hasMatches)
                {
                    Assert.Fail("Class name assertion failed. Expected element [{0}] to include a CSS class of [{1}].", fieldSelector, className);
                }
            }
            else
            {
                if (!elementClassName.Equals(className))
                {
                    Assert.Fail("Class name assertion failed. Expected element [{0]] to include a CSS class of [{1}] but current CSS class is [{2}].", fieldSelector, className, elementClassName);
                }
            }
        }

        public void On(params string[] fieldSelectors)
        {
            foreach (var fieldSelector in fieldSelectors)
            {
                On(fieldSelector);
            }
        }
    }
}