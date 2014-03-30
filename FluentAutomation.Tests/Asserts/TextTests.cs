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
                .Text(t => t.StartsWith(validText.Substring(0, 5))).In(InputsPage.TextControlSelector);

            I.Expect
                .Text(validText).In(InputsPage.TextControlSelector)
                .Text(t => t.StartsWith(validText.Substring(0, 5))).In(InputsPage.TextControlSelector);

            // Invalid
            I.Assert
                .Text(invalidText).Not.In(InputsPage.TextControlSelector)
                .Text(t => t.StartsWith(invalidText.Substring(0, 5))).Not.In(InputsPage.TextControlSelector);

            I.Expect
                .Text(invalidText).Not.In(InputsPage.TextControlSelector)
                .Text(t => t.StartsWith(invalidText.Substring(0, 5))).Not.In(InputsPage.TextControlSelector);

            // Throw due to invalid assertion
            var exception = Assert.Throws<FluentException>(() => I.Assert.Text(invalidText).In(InputsPage.TextControlSelector));
            Assert.True(exception.InnerException.Message.Contains(invalidText) && exception.InnerException.Message.Contains(validText));

            // Throw due to invalid expect
            exception = Assert.Throws<FluentExpectFailedException>(() => I.Expect.Text(invalidText).In(InputsPage.TextControlSelector));
            Assert.True(exception.Message.Contains(invalidText) && exception.Message.Contains(validText));
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
                .Text("Ontario").Not.In(InputsPage.MultiSelectControlSelector)
                .Text(t => t.StartsWith("Ont")).Not.In(InputsPage.MultiSelectControlSelector);

            I.Expect
                .Text("Manitoba").In(InputsPage.MultiSelectControlSelector)
                .Text(t => t.StartsWith("M")).In(InputsPage.MultiSelectControlSelector)
                .Text("Saskatchewan").In(InputsPage.MultiSelectControlSelector)
                .Text(t => t.StartsWith("S")).In(InputsPage.MultiSelectControlSelector)
                .Text("Ontario").Not.In(InputsPage.MultiSelectControlSelector)
                .Text(t => t.StartsWith("Ont")).Not.In(InputsPage.MultiSelectControlSelector);

            // throw due to invalid assertions
            var exception = Assert.Throws<FluentException>(() => I.Assert.Text("Ontario").In(InputsPage.MultiSelectControlSelector));
            Assert.True(exception.InnerException.Message.Contains("Ontario") && exception.InnerException.Message.Contains("Manitoba"));
        }
    }
}
