---
title: I.Append
link: Append
---
Append text to the current values of inputs and textareas. Automatically calls <code>ToString()</code> on non-string values to simplify tests.

Using `WithoutEvents()` will cause the value to be set via JavaScript and will not trigger keyup/keydown events in the browser.

```csharp
// Append text onto element value
I.Append("FluentAutomation").In("#searchBox");

// Append text without keyup/keydown events
I.Append("FluentAutomation").WithoutEvents().In("#searchBox");

// Append an integer onto element value
I.Append(6).In("#quantity");

// Append text using cached reference to element
var element = I.Find("#searchBox");
I.Append("FluentAutomation").In(element);
```
```vbnet
// TODO - Visual Basic Code Sample
```