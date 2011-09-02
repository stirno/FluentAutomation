using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentAutomation.API
{
    public partial class ExpectManager
    {
        private Browser _browser = null;
        private ExpectHandlers.ExpectValueHandler _nullHandler = null;
        
        public ExpectManager(Browser browser)
        {
            _browser = browser;
        }

        public ExpectHandlers.ExpectValueHandler Null
        {
            get
            {
                if (_nullHandler == null)
                {
                    string value = null;
                    _nullHandler = new ExpectHandlers.ExpectValueHandler(_browser, value);
                }

                return _nullHandler;
            }
        }

        public virtual ExpectHandlers.ExpectValueHandler This(string value)
        {
            return new ExpectHandlers.ExpectValueHandler(_browser, value);
        }

        public virtual ExpectHandlers.ExpectValueHandler All(params string[] values)
        {
            return new ExpectHandlers.ExpectValueHandler(_browser, values, true);
        }

        public virtual ExpectHandlers.ExpectValueHandler Any(params string[] values)
        {
            return new ExpectHandlers.ExpectValueHandler(_browser, values);
        }

        public virtual void Url(string pageUrl)
        {
            if (_browser.Url != pageUrl)
            {
                Assert.Fail(string.Format("URL Assertion failed. Expected URL {0} but actual URL is {1}.", pageUrl, _browser.Url));
            }
        }
    }
}
