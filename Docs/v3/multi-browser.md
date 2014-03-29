---
title: Multi-browser Testing
link: Multi-browser testing
---
**_Feature Status: Beta_**

Many projects require that you validate your application in multiple browsers. Over the years we've had many different methods for achieving this, but now we've made it as simple as we can.

Simply pass in multiple browser values to the `Bootstrap` call and we'll run the tests in parallel.

**Methods that return references to page elements such as `I.Find` will not work when testing with multiple browsers.** There are known issues with certain functions triggering this issue inappropriately, notably`I.Select.From()`. We are working on it!

```csharp
using FluentAutomation;
using Xunit;

namespace TestProject
{
    public  class SampleTests : FluentTest
    {
        public SampleTests()
        {
            // Execute tests in this class with Chrome, Firefox and IE
            SeleniumWebDriver.Bootstrap(
                SeleniumWebDriver.Browser.Chrome,
                SeleniumWebDriver.Browser.Firefox,
                SeleniumWebDriver.Browser.InternetExplorer
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