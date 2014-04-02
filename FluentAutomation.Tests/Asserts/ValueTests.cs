using FluentAutomation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Asserts
{
    public class ValueTests : AssertBaseTest
    {
        public ValueTests()
            : base()
        {
            InputsPage.Go();
        }

        [Fact]
        public void ValueInInputs()
        {
            // setup
            var validText = "Validation Text";
            var invalidText = "Invalid Text";
            I.Enter(validText).In(InputsPage.TextControlSelector);

            // Valid
            I.Assert
                .Value(validText).In(InputsPage.TextControlSelector)
                .Value(t => t == validText).In(InputsPage.TextControlSelector)
                .Value(validText).In(I.Find(InputsPage.TextControlSelector))
                .Value(t => t == validText).In(I.Find(InputsPage.TextControlSelector));

            I.Expect
                .Value(validText).In(InputsPage.TextControlSelector)
                .Value(t => t == validText).In(InputsPage.TextControlSelector)
                .Value(validText).In(I.Find(InputsPage.TextControlSelector))
                .Value(t => t == validText).In(InputsPage.TextControlSelector);

            // Invalid
            I.Assert
                .Value(invalidText).Not.In(InputsPage.TextControlSelector)
                .Value(t => t == invalidText).Not.In(InputsPage.TextControlSelector)
                .Value(invalidText).Not.In(I.Find(InputsPage.TextControlSelector))
                .Value(t => t == invalidText).Not.In(I.Find(InputsPage.TextControlSelector));

            I.Expect
                .Value(invalidText).Not.In(InputsPage.TextControlSelector)
                .Value(t => t == invalidText).Not.In(InputsPage.TextControlSelector)
                .Value(invalidText).Not.In(I.Find(InputsPage.TextControlSelector))
                .Value(t => t == invalidText).Not.In(I.Find(InputsPage.TextControlSelector));

            // Throw due to invalid assertion
            var exception = Assert.Throws<FluentException>(() => I.Assert.Value(invalidText).In(InputsPage.TextControlSelector));
            Assert.True(exception.InnerException.Message.Contains(invalidText) && exception.InnerException.Message.Contains(validText));

            // Throw due to invalid expect
            exception = Assert.Throws<FluentExpectFailedException>(() => I.Expect.Value(invalidText).In(InputsPage.TextControlSelector));
            Assert.True(exception.Message.Contains(invalidText) && exception.Message.Contains(validText));
        }

        [Fact]
        public void ValueInMultiSelects()
        {
            // setup
            I.Select("Manitoba", "Saskatchewan").From(InputsPage.MultiSelectControlSelector);

            I.Assert
                .Value("MB").In(InputsPage.MultiSelectControlSelector)
                .Value(t => t.StartsWith("M")).In(InputsPage.MultiSelectControlSelector)
                .Value("SK").In(InputsPage.MultiSelectControlSelector)
                .Value(t => t.StartsWith("S")).In(InputsPage.MultiSelectControlSelector)
                .Value("ON").Not.In(InputsPage.MultiSelectControlSelector)
                .Value(t => t.StartsWith("Ont")).Not.In(InputsPage.MultiSelectControlSelector);

            I.Expect
                .Value("MB").In(InputsPage.MultiSelectControlSelector)
                .Value(t => t.StartsWith("M")).In(InputsPage.MultiSelectControlSelector)
                .Value("SK").In(InputsPage.MultiSelectControlSelector)
                .Value(t => t.StartsWith("S")).In(InputsPage.MultiSelectControlSelector)
                .Value("ON").Not.In(InputsPage.MultiSelectControlSelector)
                .Value(t => t.StartsWith("Ont")).Not.In(InputsPage.MultiSelectControlSelector);

            // throw due to invalid assertions
            var exception = Assert.Throws<FluentException>(() => I.Assert.Value("ON").In(InputsPage.MultiSelectControlSelector));
            Assert.True(exception.InnerException.Message.Contains("ON") && exception.InnerException.Message.Contains("MB"));
        }

        [Fact]
        public void ValueInAlerts()
        {
            AlertsPage.Go();

            I.Click(AlertsPage.TriggerAlertSelector)
             .Assert
                .Value("Alert box").In(Alert.Message)
                .Value("Prompt box").Not.In(Alert.Message);

            I.Expect
                .Value("Alert box").In(Alert.Message)
                .Value("Prompt box").Not.In(Alert.Message);

            Assert.Throws<FluentAssertFailedException>(() => I.Assert.Value("Alert box").Not.In(Alert.Message)); // always returns immediately, so not wrapped in FluentException
            Assert.Throws<FluentException>(() => I.Assert.Value("Alert box1").In(Alert.Message));
            Assert.Throws<FluentException>(() => I.Expect.Value("Alert box").Not.In(Alert.Message));
            Assert.Throws<FluentException>(() => I.Expect.Value("Alert box1").In(Alert.Message));
            Assert.Throws<FluentException>(() => I.Expect.Value("Wat").In(Alert.Input));

            Assert.Throws<FluentException>(() => I.Assert.Value("Alert box").Not.In(Alert.Message));
            Assert.Throws<FluentException>(() => I.Assert.Value("Alert box1").In(Alert.Message));
            Assert.Throws<FluentException>(() => I.Expect.Value("Alert box").Not.In(Alert.Message));
            Assert.Throws<FluentException>(() => I.Expect.Value("Alert box1").In(Alert.Message));
            Assert.Throws<FluentException>(() => I.Expect.Value("Wat").In(Alert.Input));

            Assert.Throws<FluentException>(() => I.Expect.Value(x => x.StartsWith("Alert box")).In(Alert.Message));
            Assert.Throws<FluentException>(() => I.Expect.Value(x => x.StartsWith("Prompt box")).Not.In(Alert.Message));
        }
    }
}
