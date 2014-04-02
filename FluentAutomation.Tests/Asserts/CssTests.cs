using FluentAutomation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Asserts
{
    public class CssTests : AssertBaseTest
    {
        public CssTests()
            : base()
        {
            InputsPage.Go();
        }

        [Fact]
        public void TestClass()
        {
            I.Assert
             .Class("form-group").On(InputsPage.FormGroupDivSelector)
             .Class("not-form-group").Not.On(InputsPage.FormGroupDivSelector)
             .Class("form-group").On(I.Find(InputsPage.FormGroupDivSelector))
             .Class("not-form-group").Not.On(I.Find(InputsPage.FormGroupDivSelector));

            I.Expect
             .Class("form-group").On(InputsPage.FormGroupDivSelector)
             .Class("not-form-group").Not.On(InputsPage.FormGroupDivSelector)
             .Class("form-group").On(I.Find(InputsPage.FormGroupDivSelector))
             .Class("not-form-group").Not.On(I.Find(InputsPage.FormGroupDivSelector));

            Assert.Throws<FluentException>(() => I.Assert.Class("form-group").Not.On(InputsPage.FormGroupDivSelector));
            Assert.Throws<FluentException>(() => I.Assert.Class("not-form-group").On(InputsPage.FormGroupDivSelector));

            Assert.Throws<FluentException>(() => I.Assert.Class("form-group").Not.On(I.Find(InputsPage.FormGroupDivSelector)));
            Assert.Throws<FluentException>(() => I.Assert.Class("not-form-group").On(I.Find(InputsPage.FormGroupDivSelector)));
        }

        [Fact]
        public void TestCss()
        {
            var configWaitUntilTimeout = FluentSettings.Current.WaitUntilTimeout;
            Config.WaitUntilTimeout(TimeSpan.FromMilliseconds(50));

            I.Assert
             .Css("box-sizing").On(InputsPage.FormGroupDivSelector)
             .Css("not-box-sizing").Not.On(InputsPage.FormGroupDivSelector)
             .Css("box-sizing").On(I.Find(InputsPage.FormGroupDivSelector))
             .Css("not-box-sizing").Not.On(I.Find(InputsPage.FormGroupDivSelector))
             .Css("box-sizing", "border-box").On(InputsPage.FormGroupDivSelector)
             .Css("not-box-sizing", "border-box").Not.On(InputsPage.FormGroupDivSelector)
             .Css("box-sizing", "border-box").On(I.Find(InputsPage.FormGroupDivSelector))
             .Css("not-box-sizing", "border-box").Not.On(I.Find(InputsPage.FormGroupDivSelector));

            I.Expect
             .Css("box-sizing").On(InputsPage.FormGroupDivSelector)
             .Css("not-box-sizing").Not.On(InputsPage.FormGroupDivSelector)
             .Css("box-sizing").On(I.Find(InputsPage.FormGroupDivSelector))
             .Css("not-box-sizing").Not.On(I.Find(InputsPage.FormGroupDivSelector))
             .Css("box-sizing", "border-box").On(InputsPage.FormGroupDivSelector)
             .Css("not-box-sizing", "border-box").Not.On(InputsPage.FormGroupDivSelector)
             .Css("box-sizing", "border-box").On(I.Find(InputsPage.FormGroupDivSelector))
             .Css("not-box-sizing", "border-box").Not.On(I.Find(InputsPage.FormGroupDivSelector));

            Assert.Throws<FluentException>(() => I.Assert.Css("box-sizing").Not.On(InputsPage.FormGroupDivSelector));
            Assert.Throws<FluentException>(() => I.Assert.Css("not-box-sizing").On(InputsPage.FormGroupDivSelector));
            Assert.Throws<FluentException>(() => I.Assert.Css("box-sizing", "border-box").Not.On(InputsPage.FormGroupDivSelector));
            Assert.Throws<FluentException>(() => I.Assert.Css("not-box-sizing", "border-box").On(InputsPage.FormGroupDivSelector));

            Assert.Throws<FluentException>(() => I.Assert.Css("box-sizing").Not.On(I.Find(InputsPage.FormGroupDivSelector)));
            Assert.Throws<FluentException>(() => I.Assert.Css("not-box-sizing").On(I.Find(InputsPage.FormGroupDivSelector)));
            Assert.Throws<FluentException>(() => I.Assert.Css("box-sizing", "border-box").Not.On(I.Find(InputsPage.FormGroupDivSelector)));
            Assert.Throws<FluentException>(() => I.Assert.Css("not-box-sizing", "border-box").On(I.Find(InputsPage.FormGroupDivSelector)));

            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Css("box-sizing").Not.On(InputsPage.FormGroupDivSelector));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Css("not-box-sizing").On(InputsPage.FormGroupDivSelector));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Css("box-sizing", "border-box").Not.On(InputsPage.FormGroupDivSelector));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Css("not-box-sizing", "border-box").On(InputsPage.FormGroupDivSelector));

            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Css("box-sizing").Not.On(I.Find(InputsPage.FormGroupDivSelector)));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Css("not-box-sizing").On(I.Find(InputsPage.FormGroupDivSelector)));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Css("box-sizing", "border-box").Not.On(I.Find(InputsPage.FormGroupDivSelector)));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Css("not-box-sizing", "border-box").On(I.Find(InputsPage.FormGroupDivSelector)));

            Config.WaitUntilTimeout(configWaitUntilTimeout);
        }
    }
}
