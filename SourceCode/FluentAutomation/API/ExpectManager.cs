using System;
using FluentAutomation.API.Providers;

namespace FluentAutomation.API
{
    public partial class ExpectManager
    {
        private AutomationProvider _automation = null;
        private ExpectHandlers.ExpectValueHandler _nullHandler = null;

        public ExpectManager(AutomationProvider automation)
        {
            _automation = automation;
        }

        public ExpectHandlers.ExpectValueHandler Null
        {
            get
            {
                if (_nullHandler == null)
                {
                    string value = null;
                    _nullHandler = new ExpectHandlers.ExpectValueHandler(_automation, value);
                }

                return _nullHandler;
            }
        }

        public virtual ExpectHandlers.ExpectValueHandler This(string value)
        {
            return new ExpectHandlers.ExpectValueHandler(_automation, value);
        }

        public virtual ExpectHandlers.ExpectValueHandler All(params string[] values)
        {
            return new ExpectHandlers.ExpectValueHandler(_automation, values, true);
        }

        public virtual ExpectHandlers.ExpectValueHandler Any(params string[] values)
        {
            return new ExpectHandlers.ExpectValueHandler(_automation, values);
        }

        public virtual ExpectHandlers.ExpectCssClassHandler Class(string value)
        {
            return new ExpectHandlers.ExpectCssClassHandler(_automation, value);
        }

        public virtual void Url(string pageUrl)
        {
            if (!_automation.GetUrl().Equals(pageUrl, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new AssertException("URL Assertion failed. Expected URL {0} but actual URL is {1}.", pageUrl, _automation.GetUrl());
            }
        }
    }
}
