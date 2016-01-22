using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FluentAutomation.Tests.Check
{
    public class CheckTextTests : BaseTest
    {
        public CheckTextTests() : base()
        {
            AlertsPage.Go();
        }

        [Fact]
        public void CheckTextTest()
        {
            // Arrange

            // Act
            bool checkMatchingText = I.Check.Text("Alerts Testbed", ".container > h2");
            bool checkNotMatchingText = I.Check.Text("Input Page", ".container > h2");

            // Assert
            Assert.True(checkMatchingText);
            Assert.False(checkNotMatchingText);
        }
    }
}
