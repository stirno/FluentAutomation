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
    }
}
