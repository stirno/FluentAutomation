using FluentAutomation.Exceptions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Actions
{
    public class AppendTests : BaseTest
    {
        public AppendTests()
            : base()
        {
            InputsPage.Go();
        }

        [Fact]
        public void AppendTextToValidInput()
        {
            // set the base string so we know what the appended result will be
            I.Enter("BaseString").In(InputsPage.TextControlSelector);
            I.Enter("BaseString").In(InputsPage.TextareaControlSelector);

            // enter text, verify change fired
            I.Append("Test String").To(InputsPage.TextControlSelector)
             .Assert.Text("BaseStringTest String").In(InputsPage.TextControlSelector);
            I.Focus(InputsPage.TextareaControlSelector)
             .Assert.Text("Changed").In(InputsPage.TextChangedTextSelector);

            // no change event should be fired
            I.Append("Quick Test").WithoutEvents().To(InputsPage.TextControlSelector)
             .Assert.Text("BaseStringTest StringQuick Test").In(InputsPage.TextControlSelector);
            I.Focus(InputsPage.TextareaControlSelector)
             .Assert.Text("").In(InputsPage.TextChangedTextSelector);

            I.Append(10).To(InputsPage.TextControlSelector)
             .Assert.Text("BaseStringTest StringQuick Test10").In(InputsPage.TextControlSelector);

            I.Append("Other Test String").To(InputsPage.TextareaControlSelector)
             .Assert.Text("BaseStringOther Test String").In(InputsPage.TextareaControlSelector);
        }

        [Fact]
        public void AppendTextToInvalidInputUsingSelector()
        {
            var exception = Assert.Throws<FluentException>(() =>
            {
                I.Append("Test String").To("#text-control-fake");
            });

            Assert.True(exception.Message.Contains("Unable to find"));
        }

        [Fact]
        public void AppendTextToSelect()
        {
            // Append cannot be used on non-text elements
            var exception = Assert.Throws<FluentException>(() =>
            {
                I.Append("QA").To(InputsPage.SelectControlSelector);
            });

            Assert.True(exception.Message.Contains("only supported"));
        }
    }
}
