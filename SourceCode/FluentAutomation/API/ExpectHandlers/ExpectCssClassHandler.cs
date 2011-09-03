using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentAutomation.API.ExpectHandlers
{
    public class ExpectCssClassHandler
    {
        private Browser _browser = null;
        private string _value = string.Empty;

        public ExpectCssClassHandler(Browser browser, string value)
        {
            _browser = browser;
            _value = value;
        }

        public void On(string fieldSelector)
        {
            var element = _browser.Element(Find.BySelector(fieldSelector));
            string className = _value.Replace(".", "").Trim();

            if (element.ClassName.Contains(' '))
            {
                string[] classes = element.ClassName.Split(' ');
                bool hasMatches = false;
                foreach (var cssClass in classes)
                {
                    var cssClassString = cssClass.Trim();
                    if (!string.IsNullOrEmpty(cssClassString))
                    {
                        if (string.Equals(cssClassString, className, StringComparison.InvariantCultureIgnoreCase))
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
                if (!string.Equals(element.ClassName.Trim(), className, StringComparison.InvariantCultureIgnoreCase))
                {
                    Assert.Fail("Class name assertion failed. Expected element [{0]] to include a CSS class of [{1}] but current CSS class is [{2}].", fieldSelector, className, element.ClassName.Trim());
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
