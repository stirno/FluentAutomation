using FluentAutomation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Actions
{
    public class FindTests : BaseTest
    {
        public FindTests()
        {
            InputsPage.Go();
        }

        [Fact]
        public void FindElement()
        {
            var element = I.Find(InputsPage.TextControlSelector).Element;

            // simple assert on element to ensure it was properly loaded
            Assert.True(element.IsText);            
        }

        [Fact]
        public void AttemptToFindFakeElement()
        {
            var exception = Assert.Throws<FluentException>(() =>
            {
                var proxy = I.Find("#fake-control");
                proxy.Element.ToString(); // accessing Element executes the Find
            });

            Assert.True(exception.Message.Contains("Unable to find"));
        }
    }
}
