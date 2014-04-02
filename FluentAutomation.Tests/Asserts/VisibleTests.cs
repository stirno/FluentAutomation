using FluentAutomation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Asserts
{
    public class VisibleTests : AssertBaseTest
    {
        public VisibleTests()
            : base()
        {
            InputsPage.Go();
        }

        [Fact]
        public void TestVisible()
        {
            I.Assert
             .Visible(InputsPage.TextControlSelector)
             .Visible(I.Find(InputsPage.TextControlSelector))
             .Not.Visible(InputsPage.HiddenDivSelector)
             .Not.Visible(I.Find(InputsPage.HiddenDivSelector));

            I.Expect
             .Visible(InputsPage.TextControlSelector)
             .Visible(I.Find(InputsPage.TextControlSelector))
             .Not.Visible(InputsPage.HiddenDivSelector)
             .Not.Visible(I.Find(InputsPage.HiddenDivSelector));

            Assert.Throws<FluentException>(() => I.Assert.Visible(InputsPage.HiddenDivSelector));
            Assert.Throws<FluentException>(() => I.Assert.Not.Visible(InputsPage.TextControlSelector));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Visible(InputsPage.HiddenDivSelector));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Not.Visible(InputsPage.TextControlSelector));
        }
    }
}
