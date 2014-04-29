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

        [Fact]
        public void EnterTextInAlertConfirmPrompt()
        {
            AlertsPage.Go();

            I.Click(AlertsPage.TriggerPromptSelector)
             .Enter("Wat").In(Alert.Input)
             .Assert.Text("Clicked Prompt OK: Wat").In(AlertsPage.ResultSelector);

            // Can't enter text into a message, verify exception is thrown properly
            Assert.Throws<FluentException>(() => I.Enter("Wat2").In(Alert.Message));
        }

        [Fact]
        public void EnterTextInElementTypes()
        {
            I.Enter("test").In(InputsPage.TextEmailControlSelector)
             .Assert.Value("test").In(InputsPage.TextEmailControlSelector);

            I.Enter("test").In(InputsPage.TextSearchControlSelector)
             .Assert.Value("test").In(InputsPage.TextSearchControlSelector);

            I.Enter("test").In(InputsPage.TextUrlControlSelector)
             .Assert.Value("test").In(InputsPage.TextUrlControlSelector);

            I.Enter("test").In(InputsPage.TextTelControlSelector)
             .Assert.Value("test").In(InputsPage.TextTelControlSelector);

            I.Enter("14").In(InputsPage.TextNumberControlSelector)
             .Assert.Value("14").In(InputsPage.TextNumberControlSelector);

            I.Enter("test").In(InputsPage.TextPasswordControlSelector)
             .Assert.Value("test").In(InputsPage.TextPasswordControlSelector);
        }
    }
}
