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
        public EnterTests() : base()
        {
            I.Open("/Inputs");
        }

        [Fact]
        public void EnterTextInValidInput()
        {
            I.Enter("Test String").In("#text-control");
            I.Assert.Text("Test String").In("#text-control");

            I.Enter("Other Test String").In("#textarea-control");
            I.Assert.Text("Other Test String").In("#textarea-control");

            I.Enter(10).In("#text-control");
            I.Assert.Text("10").In("#text-control");
        }

        [Fact]
        public void EnterTextInInvalidInput()
        {
            var exception = Assert.Throws<FluentException>(() =>
            {
                I.Enter("Test String").In("#text-control-fake");
            });

            Assert.True(exception.InnerException.Message.Contains("Unable to locate"));
        }

        [Fact]
        public void EnterTextInSelect()
        {
            // Enter cannot be used on Select because it clears values, which select does not support
            Assert.Throws<InvalidElementStateException>(() =>
            {
                I.Enter("QA").In("#select-control");
            });
        }
    }
}
