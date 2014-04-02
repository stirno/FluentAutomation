using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Asserts
{
    public class CountTests : AssertBaseTest
    {
        public CountTests()
            : base()
        {
            InputsPage.Go();
        }

        [Fact]
        public void CountElements()
        {
            I.Assert
             .Count(0).Not.Of("div")
             .Count(0).Not.Of(I.Find("div"))
             .Count(0).Of("crazyElementThatDoesntExist")
             .Count(0).Of(I.Find("crazyElementThatDoesntExist"));

            I.Expect
             .Count(0).Not.Of("div")
             .Count(0).Not.Of(I.Find("div"))
             .Count(0).Of("crazyElementThatDoesntExist")
             .Count(0).Of(I.Find("crazyElementThatDoesntExist"));
        }
    }
}
