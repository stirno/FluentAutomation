using FluentAutomation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Asserts
{
    public class TextTests : AssertBaseTest
    {
        public TextTests()
            : base()
        {
            InputsPage.Go();
        }

        [Fact]
        public void TextInInputs()
        {
            // setup
            var validText = "Validation Text";
            var invalidText = "Invalid Text";
            I.Enter(validText).In(InputsPage.TextControlSelector);

            // Valid
            I.Assert
                .Text(validText).In(InputsPage.TextControlSelector)
                .Text(t => t == validText).In(InputsPage.TextControlSelector)
                .Text(validText).In(I.Find(InputsPage.TextControlSelector))
                .Text(t => t == validText).In(I.Find(InputsPage.TextControlSelector));

            I.Expect
                .Text(validText).In(InputsPage.TextControlSelector)
                .Text(t => t == validText).In(InputsPage.TextControlSelector)
                .Text(validText).In(I.Find(InputsPage.TextControlSelector))
                .Text(t => t == validText).In(InputsPage.TextControlSelector);

            // Invalid
            I.Assert
                .Text(invalidText).Not.In(InputsPage.TextControlSelector)
                .Text(t => t == invalidText).Not.In(InputsPage.TextControlSelector)
                .Text(invalidText).Not.In(I.Find(InputsPage.TextControlSelector))
                .Text(t => t == invalidText).Not.In(I.Find(InputsPage.TextControlSelector));

            I.Expect
                .Text(invalidText).Not.In(InputsPage.TextControlSelector)
                .Text(t => t == invalidText).Not.In(InputsPage.TextControlSelector)
                .Text(invalidText).Not.In(I.Find(InputsPage.TextControlSelector))
                .Text(t => t == invalidText).Not.In(I.Find(InputsPage.TextControlSelector));

            // Throw due to invalid assertion
            var exception = Assert.Throws<FluentException>(() => I.Assert.Text(invalidText).In(InputsPage.TextControlSelector));
            Assert.True(exception.InnerException.Message.Contains(invalidText) && exception.InnerException.Message.Contains(validText));
            Assert.Throws<FluentException>(() => I.Assert.Text(invalidText).In(I.Find(InputsPage.TextControlSelector)));
            Assert.Throws<FluentException>(() => I.Assert.Text(validText).Not.In(InputsPage.TextControlSelector));
            Assert.Throws<FluentException>(() => I.Assert.Text(validText).Not.In(I.Find(InputsPage.TextControlSelector)));

            // Throw due to invalid expect
            exception = Assert.Throws<FluentExpectFailedException>(() => I.Expect.Text(invalidText).In(InputsPage.TextControlSelector));
            Assert.True(exception.Message.Contains(invalidText) && exception.Message.Contains(validText));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Text(invalidText).In(I.Find(InputsPage.TextControlSelector)));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Text(validText).Not.In(InputsPage.TextControlSelector));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Text(validText).Not.In(I.Find(InputsPage.TextControlSelector)));
        }

        [Fact]
        public void TextInSelects()
        {
            // setup
            I.Select("Manitoba").From(InputsPage.SelectControlSelector);

            I.Assert
                .Text("Manitoba").In(InputsPage.SelectControlSelector)
                .Text(t => t.StartsWith("M")).In(InputsPage.SelectControlSelector);

            I.Expect
                .Text("Manitoba").In(InputsPage.SelectControlSelector)
                .Text(t => t.StartsWith("M")).In(InputsPage.SelectControlSelector);

            // throw due to invalid assertions
            var exception = Assert.Throws<FluentException>(() => I.Assert.Text("Ontario").In(InputsPage.SelectControlSelector));
            Assert.True(exception.InnerException.Message.Contains("Ontario") && exception.InnerException.Message.Contains("Manitoba"));
            Assert.Throws<FluentException>(() => I.Assert.Text(x => x == "Ontario").In(InputsPage.SelectControlSelector));
            Assert.Throws<FluentException>(() => I.Assert.Text(x => x == "Manitoba").Not.In(InputsPage.SelectControlSelector));

            // throw due to invalid expect
            exception = Assert.Throws<FluentExpectFailedException>(() => I.Expect.Text("Ontario").In(InputsPage.SelectControlSelector));
            Assert.True(exception.Message.Contains("Ontario") && exception.Message.Contains("Manitoba"));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Text(x => x == "Ontario").In(InputsPage.SelectControlSelector));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Text(x => x == "Manitoba").Not.In(InputsPage.SelectControlSelector));
        }

        [Fact]
        public void TextInMultiSelects()
        {
            // setup
            I.Select("Manitoba", "Saskatchewan").From(InputsPage.MultiSelectControlSelector);

            I.Assert
                .Text("Manitoba").In(InputsPage.MultiSelectControlSelector)
                .Text(t => t.StartsWith("M")).In(InputsPage.MultiSelectControlSelector)
                .Text("Saskatchewan").In(InputsPage.MultiSelectControlSelector)
                .Text(t => t.StartsWith("S")).In(InputsPage.MultiSelectControlSelector)
                .Text("Saskatchewan").In(I.Find(InputsPage.MultiSelectControlSelector))
                .Text(t => t.StartsWith("S")).In(I.Find(InputsPage.MultiSelectControlSelector))
                .Text("Ontario").Not.In(InputsPage.MultiSelectControlSelector)
                .Text(t => t.StartsWith("Ont")).Not.In(InputsPage.MultiSelectControlSelector)
                .Text("Ontario").Not.In(I.Find(InputsPage.MultiSelectControlSelector))
                .Text(t => t.StartsWith("Ont")).Not.In(I.Find(InputsPage.MultiSelectControlSelector));

            I.Expect
                .Text("Manitoba").In(InputsPage.MultiSelectControlSelector)
                .Text(t => t.StartsWith("M")).In(InputsPage.MultiSelectControlSelector)
                .Text("Saskatchewan").In(InputsPage.MultiSelectControlSelector)
                .Text(t => t.StartsWith("S")).In(InputsPage.MultiSelectControlSelector)
                .Text("Saskatchewan").In(I.Find(InputsPage.MultiSelectControlSelector))
                .Text(t => t.StartsWith("S")).In(I.Find(InputsPage.MultiSelectControlSelector))
                .Text("Ontario").Not.In(InputsPage.MultiSelectControlSelector)
                .Text(t => t.StartsWith("Ont")).Not.In(InputsPage.MultiSelectControlSelector)
                .Text("Ontario").Not.In(I.Find(InputsPage.MultiSelectControlSelector))
                .Text(t => t.StartsWith("Ont")).Not.In(I.Find(InputsPage.MultiSelectControlSelector));

            // throw due to invalid assertions
            var exception = Assert.Throws<FluentException>(() => I.Assert.Text("Ontario").In(InputsPage.MultiSelectControlSelector));
            Assert.True(exception.InnerException.Message.Contains("Ontario") && exception.InnerException.Message.Contains("Manitoba"));
            Assert.Throws<FluentException>(() => I.Assert.Text("Manitoba").Not.In(InputsPage.MultiSelectControlSelector));
            Assert.Throws<FluentException>(() => I.Assert.Text("Manitoba").Not.In(I.Find(InputsPage.MultiSelectControlSelector)));
            Assert.Throws<FluentException>(() => I.Assert.Text(x => x == "Ontario").In(InputsPage.MultiSelectControlSelector));
            Assert.Throws<FluentException>(() => I.Assert.Text(x => x == "Manitoba").Not.In(InputsPage.MultiSelectControlSelector));
            Assert.Throws<FluentException>(() => I.Assert.Text(x => x == "Ontario").In(I.Find(InputsPage.MultiSelectControlSelector)));
            Assert.Throws<FluentException>(() => I.Assert.Text(x => x == "Manitoba").Not.In(I.Find(InputsPage.MultiSelectControlSelector)));

            // throw due to invalid expects
            exception = Assert.Throws<FluentExpectFailedException>(() => I.Expect.Text("Ontario").In(InputsPage.MultiSelectControlSelector));
            Assert.True(exception.Message.Contains("Ontario") && exception.Message.Contains("Manitoba"));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Text("Manitoba").Not.In(InputsPage.MultiSelectControlSelector));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Text("Manitoba").Not.In(I.Find(InputsPage.MultiSelectControlSelector)));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Text(x => x == "Ontario").In(InputsPage.MultiSelectControlSelector));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Text(x => x == "Manitoba").Not.In(InputsPage.MultiSelectControlSelector));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Text(x => x == "Ontario").In(I.Find(InputsPage.MultiSelectControlSelector)));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Text(x => x == "Manitoba").Not.In(I.Find(InputsPage.MultiSelectControlSelector)));
        }

        [Fact]
        public void TextInAlerts()
        {
            AlertsPage.Go();

            I.Click(AlertsPage.TriggerAlertSelector)
             .Assert
                .Text("Alert box").In(Alert.Message)
                .Text("Prompt box").Not.In(Alert.Message);

            I.Expect
                .Text("Alert box").In(Alert.Message)
                .Text("Prompt box").Not.In(Alert.Message);

            Assert.Throws<FluentAssertFailedException>(() => I.Assert.Text("Alert box").Not.In(Alert.Message)); // always returns immediately, so not wrapped in FluentException
            Assert.Throws<FluentException>(() => I.Assert.Text("Alert box1").In(Alert.Message));
            Assert.Throws<FluentException>(() => I.Expect.Text("Alert box").Not.In(Alert.Message));
            Assert.Throws<FluentException>(() => I.Expect.Text("Alert box1").In(Alert.Message));
            Assert.Throws<FluentException>(() => I.Expect.Text("Wat").In(Alert.Input));

            Assert.Throws<FluentException>(() => I.Assert.Text("Alert box").Not.In(Alert.Message));
            Assert.Throws<FluentException>(() => I.Assert.Text("Alert box1").In(Alert.Message));
            Assert.Throws<FluentException>(() => I.Expect.Text("Alert box").Not.In(Alert.Message));
            Assert.Throws<FluentException>(() => I.Expect.Text("Alert box1").In(Alert.Message));
            Assert.Throws<FluentException>(() => I.Expect.Text("Wat").In(Alert.Input));

            Assert.Throws<FluentException>(() => I.Expect.Text(x => x.StartsWith("Alert box")).In(Alert.Message));
            Assert.Throws<FluentException>(() => I.Expect.Text(x => x.StartsWith("Prompt box")).Not.In(Alert.Message));
        }
    }
}
