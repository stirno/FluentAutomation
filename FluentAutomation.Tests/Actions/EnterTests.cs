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
            I.Open("/Inputs");
        }

        [Fact]
        public void EnterTextInValidInput()
        {
            // enter text, verify change fired
            I.Enter("Test String").In("#text-control")
             .Assert.Text("Test String").In("#text-control");
            I.Focus("#textarea-control")
             .Assert.Text("Changed").In("#text-control-changed");

            // no change event should be fired
            I.Enter("Quick Test").WithoutEvents().In("#text-control")
             .Assert.Text("Quick Test").In("#text-control");
            I.Focus("#textarea-control")
             .Assert.Text("").In("#text-control-changed");

            I.Enter("Other Test String").In("#textarea-control")
             .Assert.Text("Other Test String").In("#textarea-control");

            I.Enter(10).In("#text-control")
             .Assert.Text("10").In("#text-control");
        }

        [Fact]
        public void EnterTextInInvalidInputUsingSelector()
        {
            var exception = Assert.Throws<FluentException>(() =>
            {
                I.Enter("Test String").In("#text-control-fake");
            });

            Assert.True(exception.Message.Contains("Unable to find"));
        }

        [Fact]
        public void EnterTextInSelect()
        {
            // Enter cannot be used on non-text elements
            var exception = Assert.Throws<FluentException>(() =>
            {
                I.Enter("QA").In("#select-control");
            });

            Assert.True(exception.Message.Contains("only supported"));
        }
    }
}
