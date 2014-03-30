---
title: scriptcs
link: scriptcs
---
Access to FluentAutomation inside of <a href="http://scriptcs.net/" target="_blank">scriptcs</a> is trivially easy and very powerful. We have a custom Script Pack that provides access to the functionality you expect.

To get started, install scriptcs (per instructions on their site). The commands to the right will get a directory ready for scriptcs testing.

Create a new script, `test.csx` for example. We'll require in the Script Pack, Bootstrap our browser and get testing.

The new script can be run very easily a Command Prompt with: `scriptcs test.csx`. You'll see some debugging information from the browser and/or Selenium -- no worries -- and then you'll see the test execute and you'll get results.

```csharp
// prepare a new directory for your tests
mkdir tests
scriptcs -install ScriptCs.FluentAutomation
scriptcs -install FluentAutomation.SeleniumWebDriver

// First test!
var Test = Require<F14N>()
    .Init<FluentAutomation.SeleniumWebDriver>()
    .Bootstrap("Chrome")
    .Config(settings => {
        // Easy access to FluentAutomation.Settings values
        settings.DefaultWaitUntilTimeout = TimeSpan.FromSeconds(1);
    });

Test.Run("Hello Google", I => {
    I.Open("http://google.com");
});
```
```vbnet
// TODO - Visual Basic Code Sample
```