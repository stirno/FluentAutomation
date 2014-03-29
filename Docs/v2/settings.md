---
title: Settings
link: Settings
---
Most of the configurable settings are located on the static object FluentAutomation.Settings and can be set anywhere within your project.

`TimeSpan`	**DefaultWaitTimeout**
Length of time an unargumented `I.Wait()` will wait before continuing.

`TimeSpan` **DefaultWaitUntilThreadSleep**
Length of time to wait before re-evaluating the condition passed to `WaitUntil`.

`TimeSpan` **DefaultWaitUntilTimeout**
Length of time to wait before assuming the `WaitUntil` condition will not be met.

`bool` **ExpectIsAssert**
Whether or not `I.Expect` throws exceptions that will stop a test from continuing.

`bool` **MinimizeAllWIndowsOnTestStart**
Whether or not we ask the `Win32` API to minimize other windows before starting a test.

`bool` **ScreenshotOnFailedAction**
Whether or not we automatically take a screenshot when an action fails.

`bool` **ScreenshotOnFailedExpect**
Whether or not we automatically take a screenshot when an `Assert` or `Expect` fails.

`string` **ScreenshotPath**
Path to store screenshots on the filesystem.

`string` **UserTempDirectory**
Path used for temporary storage. Usually not necessary to change.

`bool` **WaitOnAllCommands**
Whether or not we enable implicit waits on all comamnds/actions using `I.WaitUntil`.

`bool` **WaitOnAllExpects**
Whether or not we enable implicit waits on all asserts or expects using `I.WaitUntil`.

`int` **WindowHeight**
Height of the browser window, if null will use system/profile default.

`int` **WindowWidth**
Width of the browser window, if null will use system/profile default.

`Action<FluentExpectFailedException>` **ExpectFailedCallback**
Method executed when `Settings.ExpectIsAssert` is `true` and an `Expect` fails.

```csharp
// Settings - All default values
Settings.DefaultWaitTimeout = TimeSpan.FromSeconds(1);
Settings.DefaultWaitUntilTimeout = TimeSpan.FromSeconds(30);
Settings.DefaultWaitUntilThreadSleep = TimeSpan.FromMilliseconds(100);
Settings.ExpectIsAssert = true; // changing in v2.4 to false
Settings.MinimizeAllWindowsOnTestStart = false;
Settings.ScreenshotOnFailedAction = false;
Settings.ScreenshotOnFailedExpect = false;
Settings.ScreenshotPath = Path.GetTempPath();
Settings.UserTempDirectory = Path.GetTempPath();
Settings.WaitOnAllCommands = true;
Settings.WaitOnAllExpects = true;
Settings.WindowHeight = null;
Settings.WindowWidth = null;

Settings.ExpectFailedCallback = (c) => {
    var fluentException = c.InnerException as FluentException;
    if (fluentException != null)
        Trace.WriteLine(fluentException.Message);
    else
        Trace.WriteLine(c.Message);  
};
```
```vbnet
// TODO - Visual Basic Code Sample
```