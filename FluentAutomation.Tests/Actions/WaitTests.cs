using FluentAutomation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Actions
{
    public class WaitTests : BaseTest
    {
        [Fact]
        public void Wait()
        {
            With.Wait(TimeSpan.FromMilliseconds(50)).Then.Wait();
            I.Wait(0);
            I.Wait(TimeSpan.FromMilliseconds(50));
        }

        [Fact]
        public void WaitUntil()
        {
            var waitUntilTimeout = FluentSettings.Current.WaitUntilTimeout;
            Config.WaitUntilTimeout(TimeSpan.FromMilliseconds(60));
            int controlInt = 0;
            bool toggle = false;

            // Will succeed on first access
            With
                .WaitUntil(TimeSpan.FromMilliseconds(60))
                .Then
                     .WaitUntil(() => I.Assert.True(() => true))
                     .WaitUntil(() => I.Assert.True(() => true), TimeSpan.FromMilliseconds(50))
                     .WaitUntil(() => true)
                     .WaitUntil(() => true, TimeSpan.FromMilliseconds(50));

            // Prove that a value changing over time properly triggers
            With
                .WaitUntil(TimeSpan.FromMilliseconds(60))
                .WaitInterval(TimeSpan.FromMilliseconds(25))
                .Then
                    .WaitUntil(() => this.ThrowUntilSecondCall(ref controlInt));

            controlInt = 0;
            I.WaitUntil(() => this.ReturnBoolAndThrowUntilSecondCall(ref controlInt, null), TimeSpan.FromMilliseconds(500));

            controlInt = 0;

            With
                .WaitUntil(TimeSpan.FromMilliseconds(60))
                .WaitInterval(TimeSpan.FromMilliseconds(25))
                .Then
                    .WaitUntil(() => this.ToggleBoolUntilSecondCall(ref toggle, ref controlInt));

            // Will never succeed
            Assert.Throws<FluentException>(() => I.WaitUntil(() => I.Assert.True(() => false)));
            Assert.Throws<FluentException>(() => I.WaitUntil(() => I.Assert.True(() => false), 1));
            Assert.Throws<FluentException>(() => I.WaitUntil(() => I.Assert.True(() => false), TimeSpan.FromMilliseconds(50)));

            Assert.Throws<FluentException>(() => I.WaitUntil(() => false));
            Assert.Throws<FluentException>(() => I.WaitUntil(() => false, 1));
            Assert.Throws<FluentException>(() => I.WaitUntil(() => false, TimeSpan.FromMilliseconds(50)));

            controlInt = 0;
            Assert.Throws<FluentException>(() => I.WaitUntil(() => this.ThrowUntilSecondCall(ref controlInt)));

            controlInt = 0;
            Assert.Throws<FluentException>(() => I.WaitUntil(() => this.ReturnBoolAndThrowUntilSecondCall(ref controlInt, new Exception())));

            controlInt = 0;
            Assert.Throws<FluentException>(() => I.WaitUntil(() => this.ReturnBoolAndThrowUntilSecondCall(ref controlInt, new FluentException("Eat it"))));

            controlInt = 0;
            Assert.Throws<FluentException>(() => I.WaitUntil(() => this.ReturnBoolAndThrowUntilSecondCall(ref controlInt, null), TimeSpan.FromMilliseconds(5)));

            controlInt = 0; toggle = false;
            Assert.Throws<FluentException>(() => I.WaitUntil(() => this.ToggleBoolUntilSecondCall(ref toggle, ref controlInt)));

            controlInt = 0; toggle = false;
            Assert.Throws<FluentException>(() => I.WaitUntil(() => this.ToggleBoolUntilSecondCall(ref toggle, ref controlInt), TimeSpan.FromMilliseconds(50)));

            Config.WaitUntilTimeout(waitUntilTimeout);
        }

        private void ThrowUntilSecondCall(ref int controlInt)
        {
            if (controlInt == 0)
            {
                controlInt++;
                throw new FluentException("Something happened up in here.");
            }
        }

        private bool ToggleBoolUntilSecondCall(ref bool value, ref int controlInt)
        {
            if (controlInt == 0)
            {
                controlInt++;
                return value;
            }

            value = !value;
            return value;
        }

        private bool ReturnBoolAndThrowUntilSecondCall(ref int controlInt, Exception ex)
        {
            if (controlInt == 0)
            {
                controlInt++;
                I.Wait(TimeSpan.FromMilliseconds(100));
                throw ex == null ? new FluentException("Eat it twice") : ex;
            }

            return true;
        }
    }
}
