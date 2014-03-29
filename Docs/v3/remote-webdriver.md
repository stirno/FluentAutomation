---
title: Remote WebDriver
link: Remote WebDriver
---
We fully support connecting to remote WebDriver instances (including Selenium Grid) as of the v2.2 bits. Obviously this is only supported in the SeleniumWebDriver provider.

This is accomplished by passing a URI and settings to the `Bootstrap` method in your constructor. Remote functionality becomes useful when connecting to Selenium Grid or if you want to automate a browser that doesn't run on your primary machine.

Additionally, there is support for passing a `Dictionary<string, object>` of capabilities.

```csharp
using FluentAutomation;
using System;

namespace TestProject
{
    public class MacSafariTests : FluentTest
    {
        public MacSafariTests()
        {
            SeleniumWebDriver.Bootstrap(
                new Uri("http://mac/wd/hub"), SeleniumWebDriver.Browser.Safari
            );
        }
    }
}
```
```vbnet
// TODO - Visual Basic Code Sample
```