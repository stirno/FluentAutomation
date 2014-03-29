---
title: I.Find
link: Find
---
Get a factory reference to an element. Returns a function that can be evaluated to return access to the underlying element. Used internally by all functions that target elements.

A second method, `I.FindMultiple`, exists to retrieve a collection of elements at once. It provides the same factory function but returns an `IEnumerable` instead.

Often this function is used to break through the abstraction and get direct access to the providers element representation. This can be necessary in some cases but using `I.Find` in this way is discouraged.

**Warning:** If you intend to cache an element, cache this function not its result. The result is not kept up to date with the current state of the page.

```csharp
// Find element by selector
var element = I.Find("#searchBox");

// Get reference to underlying IWebElement (Selenium)
var webElement = element() as OpenQA.Selenium.IWebElement;

// Get reference to underlying WatiN.Core.Element (WatiN)
var webElement = element() as WatiN.Core.Element;

// Find a collection of elements matching selector
var listItems = I.FindMultiple("li");
```
```vbnet
// TODO - Visual Basic Code Sample
```