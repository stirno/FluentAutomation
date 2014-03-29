using FluentAutomation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation
{
    public class FluentConfig
    {
        private static FluentConfig current = new FluentConfig();
        public static FluentConfig Current { get { return current; } }

        public FluentSettings Settings { get { return FluentSettings.Current; } }

        public FluentConfig()
        {
        }

        public FluentConfig Configure(FluentSettings settings)
        {
            FluentSettings.Current = settings;
            return this;
        }

        public FluentConfig UserTempDirectory(string tempDir)
        {
            this.Settings.UserTempDirectory = tempDir;
            return this;
        }

        public FluentConfig ScreenshotPath(string screenshotPath)
        {
            this.Settings.ScreenshotPath = screenshotPath;
            return this;
        }

        public FluentConfig ScreenshotPrefix(string prefix)
        {
            this.Settings.ScreenshotPrefix = prefix;
            return this;
        }

        public FluentConfig ScreenshotOnFailedExpect(bool screenshotOnFail)
        {
            this.Settings.ScreenshotOnFailedExpect = screenshotOnFail;
            return this;
        }

        public FluentConfig ScreenshotOnFailedAction(bool screenshotOnFail)
        {
            this.Settings.ScreenshotOnFailedAction = screenshotOnFail;
            return this;
        }

        public FluentConfig ScreenshotOnFailedAssert(bool screenshotOnFail)
        {
            this.Settings.ScreenshotOnFailedAssert = screenshotOnFail;
            return this;
        }

        public FluentConfig WaitOnAllActions(bool wait)
        {
            this.Settings.WaitOnAllActions = wait;
            return this;
        }

        public FluentConfig WaitOnAllExpects(bool wait)
        {
            this.Settings.WaitOnAllExpects = wait;
            return this;
        }

        public FluentConfig MinimizeAllWindowsOnTestStart(bool minimize)
        {
            this.Settings.MinimizeAllWindowsOnTestStart = minimize;
            return this;
        }

        public FluentConfig WindowSize(int width, int height)
        {
            this.Settings.WindowHeight = height;
            this.Settings.WindowWidth = width;
            return this;
        }

        public FluentConfig ExpectIsAssert(bool isAssert)
        {
            this.Settings.ExpectIsAssert = isAssert;
            return this;
        }

        public FluentConfig WaitTimeout(TimeSpan timeout)
        {
            this.Settings.WaitTimeout = timeout;
            return this;
        }

        public FluentConfig WaitUntilTimeout(TimeSpan timeout)
        {
            this.Settings.WaitUntilTimeout = timeout;
            return this;
        }

        public FluentConfig WaitUntilInterval(TimeSpan sleep) {
            this.Settings.WaitUntilInterval = sleep;
            return this;
        }

        public FluentConfig ContainerRegistration(Action<TinyIoC.TinyIoCContainer> registrationMethod)
        {
            this.Settings.ContainerRegistration = registrationMethod;
            return this;
        }

        public FluentConfig OnAssertFailed(Action<FluentAssertFailedException, WindowState> action)
        {
            this.Settings.OnAssertFailed = action;
            return this;
        }

        public FluentConfig OnExpectFailed(Action<FluentExpectFailedException, WindowState> action)
        {
            this.Settings.OnExpectFailed = action;
            return this;
        }
    }
}
