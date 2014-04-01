using FluentAutomation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Asserts
{
    public class BooleanTests : AssertBaseTest
    {
        [Fact]
        public void True()
        {
            I.Assert.True(() => true);
            Assert.Throws<FluentException>(() => I.Assert.True(() => false));
        }

        [Fact]
        public void False()
        {
            I.Assert.False(() => false);
            Assert.Throws<FluentException>(() => I.Assert.False(() => true));
        }
    }
}
