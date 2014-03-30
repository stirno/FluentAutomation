---
title: I.Enter
link: Enter
---
Primary method of entering text into inputs and textareas. Automatically calls `ToString()` on non-string values to simplify tests.

Using `WithoutEvents()` will cause the value to be set via JavaScript and will not trigger keyup/keydown events in the browser.

```csharp
// Enter text into element
I.Enter("FluentAutomation").In("#searchBox");

// Enter text without keyup/keydown events
I.Enter("FluentAutomation").WithoutEvents().In("#searchBox");

// Enter an integer into element
I.Enter(6).In("#quantity");

// Enter text using cached reference to element
var element = I.Find("#searchBox");
I.Enter("FluentAutomation").In(element);
```
```vbnet
// TODO - Visual Basic Code Sample
```