using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using FluentAutomation.Exceptions;

namespace FluentAutomation
{
    public class FluentSettings : IDisposable
    {
        private static readonly object _mutex = string.Empty;
        private static FluentSettings _current;

        private FluentSettings()
        {
            UniqueIdentitfier = Guid.NewGuid();

            // Toggle features on/off
            this.WaitOnAllExpects = false;
            this.WaitOnAllAsserts = true;
            this.WaitOnAllActions = true;
            this.MinimizeAllWindowsOnTestStart = false;
            this.ScreenshotOnFailedExpect = false;
            this.ScreenshotOnFailedAssert = false;
            this.ScreenshotOnFailedAction = false;
            this.ExpectIsAssert = false; // determine if Expects are treated as Asserts (v2.x behavior)

            // Browser size
            this.WindowHeight = null;
            this.WindowWidth = null;
            this.WindowMaximized = false;

            // Timeouts
            this.WaitTimeout = TimeSpan.FromSeconds(1);
            this.WaitUntilTimeout = TimeSpan.FromSeconds(5);
            this.WaitUntilInterval = TimeSpan.FromMilliseconds(100);

            // Paths
            this.UserTempDirectory = System.IO.Path.GetTempPath();
            this.ScreenshotPath = this.UserTempDirectory;

            // IoC registration
            this.ContainerRegistration = (c) => { };

            // Events
            this.OnExpectFailed = (ex, state) =>
            {
                var fluentException = ex.InnerException as FluentException;
                if (fluentException != null)
                    System.Diagnostics.Trace.WriteLine("[EXPECT FAIL] " + fluentException.Message);
                else
                    System.Diagnostics.Trace.WriteLine("[EXPECT FAIL] " + ex.Message);
            };

            this.OnAssertFailed = (ex, state) =>
            {
                var fluentException = ex.InnerException as FluentException;
                if (fluentException != null)
                    System.Diagnostics.Trace.WriteLine("[ASSERT FAIL] " + fluentException.Message);
                else
                    System.Diagnostics.Trace.WriteLine("[ASSERT FAIL] " + ex.Message);
            };

            OnFluentSettingsCreated();
        }

        ~FluentSettings()
        {
            Dispose(false);
        }

        /*-------------------------------------------------------------------*/

        public delegate void FluentSettingsCreatedEventHandler(object sender, EventArgs e);

        public delegate void FluentSettingsDisposedEventHandler(object sender, EventArgs e);

        public static event FluentSettingsCreatedEventHandler OnCreated = delegate { };

        public static event FluentSettingsDisposedEventHandler OnDisposed = delegate { };

        public static FluentSettings Current
        {
            get
            {
                if (_current == null)
                {
                    lock (_mutex)
                    {
                        if (_current == null)
                        {
                            _current = new FluentSettings();
                        }
                    }
                }

                return _current;
            }
        }

        public Guid UniqueIdentitfier { get; private set; }

        public bool WaitOnAllExpects { get; set; }

        public bool WaitOnAllAsserts { get; set; }

        public bool WaitOnAllActions { get; set; }

        public bool MinimizeAllWindowsOnTestStart { get; set; }

        public bool ExpectIsAssert { get; set; }

        public bool ScreenshotOnFailedExpect { get; set; }

        public bool ScreenshotOnFailedAssert { get; set; }

        public bool ScreenshotOnFailedAction { get; set; }

        public int? WindowHeight { get; set; }

        public int? WindowWidth { get; set; }

        public bool WindowMaximized { get; set; }

        public TimeSpan WaitTimeout { get; set; }

        public TimeSpan WaitUntilTimeout { get; set; }

        public TimeSpan WaitUntilInterval { get; set; }

        public string ScreenshotPath { get; set; }

        public string ScreenshotPrefix { get; set; }

        public string UserTempDirectory { get; set; }

        public Action<TinyIoC.TinyIoCContainer> ContainerRegistration { get; set; }

        public Action<FluentExpectFailedException, WindowState> OnExpectFailed { get; set; }

        public Action<FluentAssertFailedException, WindowState> OnAssertFailed { get; set; }

        public bool InDebugMode { get; set; }

        public bool IsDryRun { get; set; }

        public bool Disposed { get; private set; }

        /*-------------------------------------------------------------------*/

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal FluentSettings Clone()
        {
            return (FluentSettings)this.MemberwiseClone();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    // Dispose any managed objects
                    // ...
                }

                // Now disposed of any unmanaged objects
                // ...

                Disposed = true;
                OnFluentSettingsDisposed();
            }
        }

        /*-------------------------------------------------------------------*/

        private void OnFluentSettingsCreated()
        {
            OnCreated(this, EventArgs.Empty);
        }

        private void OnFluentSettingsDisposed()
        {
            OnDisposed(this, EventArgs.Empty);
        }
    }
}
