---
title: Getting Started
link: Getting Started
---
FluentAutomation is implemented using one of two supported automation providers - <a href="http://seleniumhq.org" target="_blank">Selenium WebDriver</a> or <a href="http://watin.org" target="_blank">WatiN</a>. Selenium is the preferred provider and the most developed. WatiN is provided as an alternative method of automation Internet Explorer. PhantomJS support has been moved into the Selenium package as a browser target.

You'll need a unit test framework as well. I use either <a href="https://github.com/xunit/xunit" target="_blank">xUnit.net</a> or <a href="http://nunit.org/" target="_blank">NUnit</a> in most projects. MSTest (the default in Visual Studio Test Projects) is also supported. We don't exhaustively test on every unit test library out there so let us know if your favorite framework doesn't work!

Once you've decided which provider to use and have your test framework ready, it is super easy to start testing with FluentAutomation -- use NuGet!

The commands to the right can help you install via the NuGet Package Manager Console or you can just search for FluentAutomation in the 'Manage NuGet Packages' dialog within Visual Studio. NuGet will handle downloading all the dependencies and setting up the project.

If you're using xUnit.net, copy the sample test on the right into a new class file and you're ready to go!

**Test Setup**

Your test classes need to inherit from `FluentAutomation.FluentTest` and call your providers appropriate `Bootstrap` method. In most test frameworks you'll do this in the constructor of your class, as shown in the sample.

For Selenium, the `Bootstrap` method takes the browser target as an argument. You can pass multiple browsers if you would like to run the same test in each.

```csharp
/*-------- NuGet Package Manager Console --------*/

// Install the Selenium WebDriver provider
Install-Package FluentAutomation.SeleniumWebDriver

// Install the WatiN provider
Install-Package FluentAutomation.WatiN

/*--------------- SampleTests.cs ---------------*/

using FluentAutomation;
using Xunit;

namespace TestProject
{
    public  class SampleTests : FluentTest
    {
        public SampleTests()
        {
            SeleniumWebDriver.Bootstrap(
                SeleniumWebDriver.Browser.Chrome
            );
        }
&nbsp;
        [Fact]
        public void Test1()
        {
            I.Open("http://google.com/");
            // get busy testing!
        }
    }
}
```
```vbnet
// TODO - Visual Basic Code Sample
```
