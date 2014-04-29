using FluentAutomation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Base
{
    // Not public because we don't want this test running in the standard suite and we aren't using Traits yet
    // to group them. Maybe later.
    class MultiBrowserTests : FluentTest
    {
        private MultiBrowserTests()
        {
            FluentAutomation.SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome, SeleniumWebDriver.Browser.Firefox);
        }

        [Fact]
        /// See https://github.com/stirno/FluentAutomation/issues/104
        public void AssertShouldFailTest()
        {
            Assert.Throws<FluentException>(() =>
            {
                I.Open("http://google.com/")
                 .Assert
                    .Class("wowza").On("input[type='text']");
            });
        }
    }
}
