using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace FluentAutomation.Tests.Check
{
    public class CheckVisibilityTests : BaseTest
    {
        public CheckVisibilityTests() : base()
        {
            AlertsPage.Go();
        }

        [Fact]
        public void TestVisibility()
        {
            // Arrange

            // Act
            bool checkVisibleElement = I.Check.Visible(".container > h2");
            bool checkNotVisibleElement = I.Check.Visible(".this-does-not-exist-trolololol");

            // Assert
            Assert.True(checkVisibleElement);
            Assert.False(checkNotVisibleElement);
        }
    }
}
