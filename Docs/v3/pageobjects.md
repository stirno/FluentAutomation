---
title: PageObjects
link: PageObjects
---
Anyone who has done any serious amount of automated testing will tell you that the largest challenge we all face is fragile, hard to maintain tests.

To combat this, it can be useful to use `PageObject` to group your actions/expects/asserts. This gives you functional code that can be reused in any number of tests.

With the new first-class `PageObject` support, we provide some simple but useful built-in functions such as `Go` and `Switch`. Also included, a validation function tied to the `this.At` property.

Any time a `PageObject` navigation is triggered, or any time `Switch` is used, the included `At` functino will execute providing an easy hook to make sure all tests execute with the underlying browser and application in the same state, every time.

If you have any ideas for or comments on the new <code>PageObject</code> functionality, let us know! Its a new feature and we'd love some feedback.

<div style="height: 430px"></div>

```csharp
public class BingSearchPage : PageObject<BingSearchPage>
{
    public BingSearchPage(FluentTest test)
        : base(test)
    {
        Url = "http://bing.com/";
        At = () => I.Expect.Exists(SearchInput);
    }
    
    public BingSearchResultsPage Search(string searchText)
    {
        I.Enter(searchText).In(SearchInput);
        I.Press("{ENTER}");
        return this.Switch<BingSearchResultsPage>();
    }
    
    private const string SearchInput = "input[title='Enter your search term']";
}

public class BingSearchResultsPage : PageObject<BingSearchResultsPage>
{
    public BingSearchResultsPage(FluentTest test)
        : base(test)
    {
        At = () => I.Expect.Exists(SearchResultsContainer);
    }
    
    public BingSearchResultsPage FindResultUrl(string url)
    {
        I.Expect.Exists(string.Format(ResultUrlLink, url));
        return this;
    }
    
    private const string SearchResultsContainer = "#b_results";
    private const string ResultUrlLink = "a[href='{0}']";
}

public class SampleTest : FluentTest
{
    public SampleTest()
    {
        SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome);
    }
    
    [Fact]
    public void SearchForFluentAutomation()
    {
        new BingSearchPage(this)
            .Go()
            .Search("FluentAutomation")
            .FindResultUrl("http://fluent.stirno.com/");
    }
}
```
```vbnet
// TODO - Visual Basic Code Sample
```
