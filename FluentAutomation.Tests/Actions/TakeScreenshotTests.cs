using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xunit;
using System.Globalization;
using FluentAutomation.Exceptions;

namespace FluentAutomation.Tests.Actions
{
    public class TakeScreenshotTests : BaseTest
    {
        private string tempPath = null;

        public TakeScreenshotTests()
            : base()
        {
            tempPath = Path.GetTempPath();
            Config.ScreenshotPath(tempPath);

            TextPage.Go();
        }

        [Fact]
        public void TakeScreenshot()
        {
            var screenshotName = string.Format(CultureInfo.CurrentCulture, "TakeScreenshot_{0}", DateTimeOffset.Now.Date.ToFileTime());
            var filepath = this.tempPath + screenshotName + ".png";

            I.Assert.False(() => File.Exists(filepath));

            I.TakeScreenshot(screenshotName)
             .Assert
                .True(() => File.Exists(filepath))
                .True(() => new FileInfo(filepath).Length > 0);

            File.Delete(filepath);
        }
        
        [Fact]
        public void ScreenshotOnFailedAction()
        {
            var c = Config.Settings.ScreenshotOnFailedAction;
            Config.ScreenshotOnFailedAction(true);

            Assert.Throws<FluentException>(() => I.Click("#nope"));

            var screenshotName = string.Format(CultureInfo.CurrentCulture, "ActionFailed_{0}", DateTimeOffset.Now.Date.ToFileTime());
            var filepath = this.tempPath + screenshotName + ".png";
            I.Assert
             .True(() => File.Exists(filepath))
             .True(() => new FileInfo(filepath).Length > 0);

            File.Delete(filepath);

            Config.ScreenshotOnFailedAction(c);            
        }

        [Fact]
        public void ScreenshotOnFailedAssert()
        {
            var c = Config.Settings.ScreenshotOnFailedAssert;
            Config.ScreenshotOnFailedAssert(true);

            Assert.Throws<FluentException>(() => I.Assert.True(() => false));

            var screenshotName = string.Format(CultureInfo.CurrentCulture, "AssertFailed_{0}", DateTimeOffset.Now.Date.ToFileTime());
            var filepath = this.tempPath + screenshotName + ".png";
            I.Assert
             .True(() => File.Exists(filepath))
             .True(() => new FileInfo(filepath).Length > 0);

            File.Delete(filepath);

            Config.ScreenshotOnFailedAssert(c);
        }

        /*
        [Fact]
        public void ScreenshotOnFailedExpect()
        {
            var c = Config.Settings.ScreenshotOnFailedExpect;
            Config.ScreenshotOnFailedExpect(true);
            
            I.Expect.True(() => false);

            var screenshotName = string.Format(CultureInfo.CurrentCulture, "ExpectFailed_{0}", DateTimeOffset.Now.Date.ToFileTime());
            var filepath = this.tempPath + screenshotName + ".png";
            I.Assert
                .True(() => File.Exists(filepath))
                .True(() => new FileInfo(filepath).Length > 0);

            File.Delete(filepath);

            Config.ScreenshotOnFailedExpect(c);
        }
        */
    }
}
