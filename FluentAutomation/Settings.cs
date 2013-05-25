using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAutomation
{
    public static class Settings
    {
        public static Action<TinyIoC.TinyIoCContainer> Registration = (c) => { };

        private static Dictionary<string, object> providerData = new Dictionary<string, object>();
        internal static Dictionary<string, object> ProviderData
        {
            get { return providerData; }
        }

        private static string userTempDirectory = System.IO.Path.GetTempPath();
        public static string UserTempDirectory
        {
            get
            {
                return userTempDirectory;
            }

            set
            {
                userTempDirectory = value;
            }
        }

        private static string screenshotPath = System.IO.Path.GetTempPath();
        public static string ScreenshotPath
        {
            get
            {
                return screenshotPath;
            }

            set
            {
                screenshotPath = value;
            } 
        }

        private static bool screenshotOnFailedExpect = false;
        public static bool ScreenshotOnFailedExpect
        {
            get
            {
                return screenshotOnFailedExpect;
            }

            set
            {
                screenshotOnFailedExpect = value;
            }
        }

        private static bool screenshotOnFailedAction = false;
        public static bool ScreenshotOnFailedAction
        {
            get
            {
                return screenshotOnFailedAction;
            }

            set
            {
                screenshotOnFailedAction = value;
            }
        }

        private static TimeSpan defaultWaitTimeout = TimeSpan.FromSeconds(1);
        public static TimeSpan DefaultWaitTimeout
        {
            get
            {
                return defaultWaitTimeout;
            }

            set
            {
                defaultWaitTimeout = value;
            }
        }

        private static TimeSpan defaultWaitUntilTimeout = TimeSpan.FromSeconds(30);
        /// <summary>
        /// Time to wait before assuming the provided WaitUntil() condition will never be reached. Defaults to 30 seconds.
        /// </summary>
        public static TimeSpan DefaultWaitUntilTimeout
        {
            get
            {
                return defaultWaitUntilTimeout;
            }

            set
            {
                defaultWaitUntilTimeout = value;
            }
        }

        private static TimeSpan defaultWaitUntilThreadSleep = TimeSpan.FromMilliseconds(100);
        /// <summary>
        /// Time to wait before attempting to validate the provided condition for WatiUntil(). Defaults to 100 milliseconds.
        /// </summary>
        public static TimeSpan DefaultWaitUntilThreadSleep
        {
            get
            {
                return defaultWaitUntilThreadSleep;
            }

            set
            {
                defaultWaitUntilThreadSleep = value;
            }
        }

        private static bool minimizeAllWindowsOnTestStart = true;
        /// <summary>
        /// Determines whether or not windows will automatically be minimized on start of test execution and reverted when finished. Defaults to true.
        /// </summary>
        public static bool MinimizeAllWindowsOnTestStart
        {
            get
            {
                return minimizeAllWindowsOnTestStart;
            }

            set
            {
                minimizeAllWindowsOnTestStart = value;
            }
        }

        private static int? windowHeight = null;
        /// <summary>
        /// Determines the height of the automated browser window. Defaults to null, which will use the provider defaults.
        /// </summary>
        public static int? WindowHeight
        {
            get
            {
                return windowHeight;
            }

            set
            {
                windowHeight = value;
            }
        }

        private static int? windowWidth = null;
        /// <summary>
        /// Determines the width of the automated browser window. Defaults to null, which will use the provider defaults.
        /// </summary>
        public static int? WindowWidth
        {
            get
            {
                return windowWidth;
            }

            set
            {
                windowWidth = value;
            }
        }
    }
}
