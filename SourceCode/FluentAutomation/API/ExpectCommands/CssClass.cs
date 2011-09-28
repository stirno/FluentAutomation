// <copyright file="CssClass.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System.Linq;
using FluentAutomation.API.Enumerations;
using FluentAutomation.API.Exceptions;
using FluentAutomation.API.Providers;

namespace FluentAutomation.API.ExpectCommands
{
    /// <summary>
    /// CssClass Expect Commands
    /// </summary>
    public class CssClass
    {
        private AutomationProvider _automation = null;
        private string _value = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="CssClass"/> class.
        /// </summary>
        /// <param name="automation">The automation.</param>
        /// <param name="value">The value.</param>
        public CssClass(AutomationProvider automation, string value)
        {
            _automation = automation;
            _value = value;
        }

        /// <summary>
        /// Expects the specified field has a specific class.
        /// </summary>
        /// <param name="fieldSelector">The field selector.</param>
        public void On(string fieldSelector)
        {
            On(fieldSelector, MatchConditions.None);
        }

        /// <summary>
        /// Expects the specified field has a specific class.
        /// </summary>
        /// <param name="fieldSelector">The field selector.</param>
        /// <param name="conditions">The conditions.</param>
        public void On(string fieldSelector, MatchConditions conditions)
        {
            var element = _automation.GetElement(fieldSelector, conditions);
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
                    _automation.TakeScreenshot();
                    throw new AssertException("Class name assertion failed. Expected element [{0}] to include a CSS class of [{1}].", fieldSelector, className);
                }
            }
            else
            {
                if (!elementClassName.Equals(className))
                {
                    _automation.TakeScreenshot();
                    throw new AssertException("Class name assertion failed. Expected element [{0]] to include a CSS class of [{1}] but current CSS class is [{2}].", fieldSelector, className, elementClassName);
                }
            }
        }

        /// <summary>
        /// Expects the specified fields have a specific class.
        /// </summary>
        /// <param name="fieldSelectors">The field selectors.</param>
        public void On(params string[] fieldSelectors)
        {
            foreach (var fieldSelector in fieldSelectors)
            {
                On(fieldSelector);
            }
        }
    }
}