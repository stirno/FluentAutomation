using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Asserts
{
    public class ExistsTests : AssertBaseTest
    {
        public ExistsTests()
            : base()
        {
            InputsPage.Go();
        }

        [Fact]
        public void ElementExists()
        {
            I.Assert
             .Exists("div")
             .Not.Exists("crazyElementThatDoesntExist")
             .Exists(I.Find("div"))
             .Not.Exists(I.Find("crazyElementThatDoesntExist"));

            I.Expect
             .Exists("div")
             .Not.Exists("crazyElementThatDoesntExist")
             .Exists(I.Find("div"))
             .Not.Exists(I.Find("crazyElementThatDoesntExist"));
        }
    }
}
