using FluentAutomation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation
{
    public class FluentSettings
    {
        private static FluentSettings current = new FluentSettings();
        public static FluentSettings Current
        {
            get { return current; }
            set { current = value; }
        }

        public FluentSettings()
        {
            // Toggle features on/off
            this.WaitOnAllExpects = false;
            this.WaitOnAllAsserts = true;
            this.WaitOnAllActions = true;
            this.MinimizeAllWindowsOnTestStart = false;
            this.ScreenshotOnFailedExpect = false;
            this.ScreenshotOnFailedAction = false;
            this.ExpectIsAssert = false; // determine if Expects are treated as Asserts (v2.x behavior)

            // browser size
            this.WindowHeight = null;
            this.WindowWidth = null;

            // timeouts
            this.DefaultWaitTimeout = TimeSpan.FromSeconds(1);
            this.DefaultWaitUntilTimeout = TimeSpan.FromSeconds(5);
            this.DefaultWaitUntilThreadSleep = TimeSpan.FromMilliseconds(100);

            // paths
            this.UserTempDirectory = System.IO.Path.GetTempPath();
            this.ScreenshotPath = this.UserTempDirectory;

            // IoC registration
            this.ContainerRegistration = (c) => { };

            // events
            this.OnExpectFailed = (c) => {};
            this.OnAssertFailed = (c) => {};
        }

        public bool WaitOnAllExpects { get; set; }
        public bool WaitOnAllAsserts { get; set; }
        public bool WaitOnAllActions { get; set; }
        public bool MinimizeAllWindowsOnTestStart { get; set; }
        public bool ExpectIsAssert { get; set; }
        public bool ScreenshotOnFailedExpect { get; set; }
        public bool ScreenshotOnFailedAction { get; set; }
        public int? WindowHeight { get; set; }
        public int? WindowWidth { get; set; }
        public TimeSpan DefaultWaitTimeout { get; set; }
        public TimeSpan DefaultWaitUntilTimeout { get; set; }
        public TimeSpan DefaultWaitUntilThreadSleep { get; set; }
        public string ScreenshotPath { get; set; }
        public string UserTempDirectory { get; set; }
        public Action<TinyIoC.TinyIoCContainer> ContainerRegistration { get; set; }
        public Action<FluentExpectFailedException> OnExpectFailed { get; set; }
        public Action<FluentAssertFailedException> OnAssertFailed { get; set; }
    }
}
