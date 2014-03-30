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
            I.Open("/Inputs");
        }

        [Fact]
        public void AppendTextToValidInput()
        {
            // set the base string so we know what the appended result will be
            I.Enter("BaseString").In("#text-control");
            I.Enter("BaseString").In("#textarea-control");

            // enter text, verify change fired
            I.Append("Test String").To("#text-control")
             .Assert.Text("BaseStringTest String").In("#text-control");
            I.Focus("#textarea-control")
             .Assert.Text("Changed").In("#text-control-changed");

            // no change event should be fired
            I.Append("Quick Test").WithoutEvents().To("#text-control")
             .Assert.Text("BaseStringTest StringQuick Test").In("#text-control");
            I.Focus("#textarea-control")
             .Assert.Text("").In("#text-control-changed");

            I.Append(10).To("#text-control")
             .Assert.Text("BaseStringTest StringQuick Test10").In("#text-control");

            I.Append("Other Test String").To("#textarea-control")
             .Assert.Text("BaseStringOther Test String").In("#textarea-control");
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
                I.Append("QA").To("#select-control");
            });

            Assert.True(exception.Message.Contains("only supported"));
        }
    }
}
