using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FluentAutomation.Tests.Check
{
    public class CheckExistenceTests : BaseTest
    {
        public CheckExistenceTests() : base()
        {
            AlertsPage.Go();
        }

        [Fact]
        public void TestExistence()
        {
            // Arrange

            // Act
            bool CheckExistingElement = I.Check.Exist(".container > h2");
            bool CheckNotExistingElement = I.Check.Exist(".container > h3");

            // Assert
            Assert.True(CheckExistingElement);
            Assert.False(CheckNotExistingElement);
        }
    }
}
