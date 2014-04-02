using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Asserts
{
    public class UrlTests : AssertBaseTest
    {
        public UrlTests()
            : base()
        {
            I.Open(this.SiteUrl);
        }

        [Fact]
        public void TestUrl()
        {
            I.Assert
             .Url(this.SiteUrl)
             .Url(x => x.Scheme == "http");

            I.Expect
             .Url(this.SiteUrl)
             .Url(x => x.Scheme == "http");
        }
    }
}
