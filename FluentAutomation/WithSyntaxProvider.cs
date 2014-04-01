using FluentAutomation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation
{
    public class WithSyntaxProvider
    {
        protected readonly IActionSyntaxProvider actionSyntaxProvider;
        protected readonly FluentSettings inlineSettings = null;

        public WithSyntaxProvider(IActionSyntaxProvider actionSyntaxProvider)
        {
            this.actionSyntaxProvider = actionSyntaxProvider;
            this.inlineSettings = FluentSettings.Current.Clone();
        }

        public WithSyntaxProvider Wait(int seconds)
        {
            return this.Wait(TimeSpan.FromSeconds(seconds));
        }

        public WithSyntaxProvider Wait(TimeSpan timeout)
        {
            this.inlineSettings.WaitTimeout = timeout;
            return this;
        }

        public WithSyntaxProvider WaitUntil(int seconds)
        {
            return this.WaitUntil(TimeSpan.FromSeconds(seconds));
        }

        public WithSyntaxProvider WaitUntil(TimeSpan timeout)
        {
            this.inlineSettings.WaitUntilTimeout = timeout;
            return this;
        }

        public WithSyntaxProvider WaitInterval(int seconds)
        {
            return this.WaitInterval(TimeSpan.FromSeconds(seconds));
        }

        public WithSyntaxProvider WaitInterval(TimeSpan interval)
        {
            this.inlineSettings.WaitUntilInterval = interval;
            return this;
        }

        public WithSyntaxProvider WaitOnAllActions(bool wait)
        {
            this.inlineSettings.WaitOnAllActions = wait;
            return this;
        }

        public WithSyntaxProvider WaitOnAllExpects(bool wait)
        {
            this.inlineSettings.WaitOnAllExpects = wait;
            return this;
        }

        public WithSyntaxProvider WaitOnAllAsserts(bool wait)
        {
            this.inlineSettings.WaitOnAllAsserts = wait;
            return this;
        }

        public WithSyntaxProvider ScreenshotPath(string screenshotPath)
        {
            this.inlineSettings.ScreenshotPath = screenshotPath;
            return this;
        }

        public WithSyntaxProvider ScreenshotPrefix(string prefix)
        {
            this.inlineSettings.ScreenshotPrefix = prefix;
            return this;
        }

        public WithSyntaxProvider ScreenshotOnFailedAction(bool screenshotOnFail)
        {
            this.inlineSettings.ScreenshotOnFailedAction = screenshotOnFail;
            return this;
        }

        public WithSyntaxProvider ScreenshotOnFailedExpect(bool screenshotOnFail)
        {
            this.inlineSettings.ScreenshotOnFailedExpect = screenshotOnFail;
            return this;
        }

        public WithSyntaxProvider ScreenshotOnFailedAssert(bool screenshotOnFail)
        {
            this.inlineSettings.ScreenshotOnFailedAssert = screenshotOnFail;
            return this;
        }

        public WithSyntaxProvider WindowSize(int width, int height)
        {
            this.inlineSettings.WindowHeight = height;
            this.inlineSettings.WindowWidth = width;
            return this;
        }

        public IActionSyntaxProvider Then
        {
            get
            {
                var actionSyntaxProvider = (ActionSyntaxProvider)this.actionSyntaxProvider;
                actionSyntaxProvider.commandProvider.WithConfig(this.inlineSettings);
                return this.actionSyntaxProvider;
            }
        }
    }
}
