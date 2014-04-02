using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Base
{
    public class FluentTestTests : BaseTest
    {
        public FluentTestTests()
            : base()
        {
        }

        [Fact]
        public void ProviderIsAvailable()
        {
            Assert.True(this.Provider != null);
        }
    }
}
