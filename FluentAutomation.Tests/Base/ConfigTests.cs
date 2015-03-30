using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Base
{
    public class ConfigTests : BaseTest
    {
        [Fact]
        public void TestConfig()
        {
            Config.WaitUntilTimeout(FluentSettings.Current.WaitUntilTimeout)
                .WaitUntilInterval(FluentSettings.Current.WaitUntilInterval)
                .WaitTimeout(FluentSettings.Current.WaitTimeout)
                .UserTempDirectory(FluentSettings.Current.UserTempDirectory)
                .WaitOnAllActions(FluentSettings.Current.WaitOnAllActions)
                .WaitOnAllExpects(FluentSettings.Current.WaitOnAllExpects)
                .WindowSize(FluentSettings.Current.WindowWidth.Value, FluentSettings.Current.WindowHeight.Value)
                .ScreenshotOnFailedAction(FluentSettings.Current.ScreenshotOnFailedAction)
                .ScreenshotOnFailedAssert(FluentSettings.Current.ScreenshotOnFailedAssert)
                .ScreenshotOnFailedExpect(FluentSettings.Current.ScreenshotOnFailedExpect)
                .OnAssertFailed(FluentSettings.Current.OnAssertFailed)
                .OnExpectFailed(FluentSettings.Current.OnExpectFailed)
                .MinimizeAllWindowsOnTestStart(FluentSettings.Current.MinimizeAllWindowsOnTestStart)
                .ExpectIsAssert(FluentSettings.Current.ExpectIsAssert)
                .ContainerRegistration(FluentSettings.Current.ContainerRegistration);
        }
    }
}
