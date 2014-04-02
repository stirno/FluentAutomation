using FluentAutomation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Asserts
{
    public class AttributeTests : AssertBaseTest
    {
        public AttributeTests()
            : base()
        {
            InputsPage.Go();
        }

        [Fact]
        public void TestAttributes()
        {
            var configWaitUntilTimeout = FluentSettings.Current.WaitUntilTimeout;
            Config.WaitUntilTimeout(TimeSpan.FromMilliseconds(50));

            I.Assert
             .Attribute("id").On(InputsPage.TextareaControlSelector)
             .Attribute("not-id").Not.On(InputsPage.TextareaControlSelector)
             .Attribute("id").On(I.Find(InputsPage.TextareaControlSelector))
             .Attribute("not-id").Not.On(I.Find(InputsPage.TextareaControlSelector))
             .Attribute("id", "textarea-control").On(InputsPage.TextareaControlSelector)
             .Attribute("not-id", "textarea-control").Not.On(InputsPage.TextareaControlSelector)
             .Attribute("id", "textarea-control").On(I.Find(InputsPage.TextareaControlSelector))
             .Attribute("not-id", "textarea-control").Not.On(I.Find(InputsPage.TextareaControlSelector));

            I.Expect
             .Attribute("id").On(InputsPage.TextareaControlSelector)
             .Attribute("not-id").Not.On(InputsPage.TextareaControlSelector)
             .Attribute("id").On(I.Find(InputsPage.TextareaControlSelector))
             .Attribute("not-id").Not.On(I.Find(InputsPage.TextareaControlSelector))
             .Attribute("id", "textarea-control").On(InputsPage.TextareaControlSelector)
             .Attribute("not-id", "textarea-control").Not.On(InputsPage.TextareaControlSelector)
             .Attribute("id", "textarea-control").On(I.Find(InputsPage.TextareaControlSelector))
             .Attribute("not-id", "textarea-control").Not.On(I.Find(InputsPage.TextareaControlSelector));

            Assert.Throws<FluentException>(() => I.Assert.Attribute("id").Not.On(InputsPage.TextareaControlSelector));
            Assert.Throws<FluentException>(() => I.Assert.Attribute("not-id").On(InputsPage.TextareaControlSelector));
            Assert.Throws<FluentException>(() => I.Assert.Attribute("id", "textarea-control").Not.On(InputsPage.TextareaControlSelector));
            Assert.Throws<FluentException>(() => I.Assert.Attribute("not-id", "textarea-control").On(InputsPage.TextareaControlSelector));

            Assert.Throws<FluentException>(() => I.Assert.Attribute("id").Not.On(I.Find(InputsPage.TextareaControlSelector)));
            Assert.Throws<FluentException>(() => I.Assert.Attribute("not-id").On(I.Find(InputsPage.TextareaControlSelector)));
            Assert.Throws<FluentException>(() => I.Assert.Attribute("id", "textarea-control").Not.On(I.Find(InputsPage.TextareaControlSelector)));
            Assert.Throws<FluentException>(() => I.Assert.Attribute("not-id", "textarea-control").On(I.Find(InputsPage.TextareaControlSelector)));

            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Attribute("id").Not.On(InputsPage.TextareaControlSelector));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Attribute("not-id").On(InputsPage.TextareaControlSelector));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Attribute("id", "textarea-control").Not.On(InputsPage.TextareaControlSelector));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Attribute("not-id", "textarea-control").On(InputsPage.TextareaControlSelector));

            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Attribute("id").Not.On(I.Find(InputsPage.TextareaControlSelector)));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Attribute("not-id").On(I.Find(InputsPage.TextareaControlSelector)));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Attribute("id", "textarea-control").Not.On(I.Find(InputsPage.TextareaControlSelector)));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Attribute("not-id", "textarea-control").On(I.Find(InputsPage.TextareaControlSelector)));

            Config.WaitUntilTimeout(configWaitUntilTimeout);
        }
    }
}
