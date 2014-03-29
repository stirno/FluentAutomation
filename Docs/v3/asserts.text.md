---
title: I.Assert.Text
link: Text
---
Assert that an element matching selector has the specified text. Works with any DOM element that has `innerHTML` or can provides its contents/value via text.

Supports anonymous functions that return `true` or `false`.

```csharp
// Header tag set to FluentAutomation
I.Assert.Text("FluentAutomation").In("header");

// Content longer than 50 characters
I.Assert.Text((text) => text.Length > 50).In("#content");
```
```vbnet
// TODO - Visual Basic Code Sample
```