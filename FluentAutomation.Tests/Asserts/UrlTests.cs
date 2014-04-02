using FluentAutomation.Exceptions;
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
             .Url(x => x.Scheme == "http")
             .Not.Url("http://google.com")
             .Not.Url(x => x.Scheme == "https");

            I.Expect
             .Url(this.SiteUrl)
             .Url(x => x.Scheme == "http")
             .Not.Url("http://google.com")
             .Not.Url(x => x.Scheme == "https");

            Assert.Throws<FluentException>(() => I.Assert.Not.Url(this.SiteUrl));
            Assert.Throws<FluentException>(() => I.Assert.Not.Url(x => x.Scheme == "http"));

            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Not.Url(this.SiteUrl));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Not.Url(x => x.Scheme == "http"));
        }
    }
}
