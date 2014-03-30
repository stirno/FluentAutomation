using FluentAutomation.Exceptions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Actions
{
    public class EnterTests : BaseTest
    {
        public EnterTests()
            : base()
        {
            InputsPage.Go();
        }

        [Fact]
        public void EnterTextInValidInput()
        {
            // enter text, verify change fired
            I.Enter("Test String").In(InputsPage.TextControlSelector)
             .Assert.Text("Test String").In(InputsPage.TextControlSelector);
            I.Focus(InputsPage.TextareaControlSelector)
             .Assert.Text("Changed").In(InputsPage.TextChangedTextSelector);

            // no change event should be fired
            I.Enter("Quick Test").WithoutEvents().In(InputsPage.TextControlSelector)
             .Assert.Text("Quick Test").In(InputsPage.TextControlSelector);
            I.Focus(InputsPage.TextareaControlSelector)
             .Assert.Text("").In(InputsPage.TextChangedTextSelector);

            I.Enter("Other Test String").In(InputsPage.TextareaControlSelector)
             .Assert.Text("Other Test String").In(InputsPage.TextareaControlSelector);

            I.Enter(10).In(InputsPage.TextControlSelector)
             .Assert.Text("10").In(InputsPage.TextControlSelector);
        }

        [Fact]
        public void EnterTextInInvalidInputUsingSelector()
        {
            var exception = Assert.Throws<FluentException>(() => I.Enter("Test String").In("#text-control-fake"));
            Assert.True(exception.Message.Contains("Unable to find"));
        }

        [Fact]
        public void EnterTextInSelect()
        {
            // Enter cannot be used on non-text elements
            var exception = Assert.Throws<FluentException>(() => I.Enter("QA").In(InputsPage.SelectControlSelector));
            Assert.True(exception.Message.Contains("only supported"));
        }
    }
}
