---
title: I.Click
link: Click
---
Click an element by selector, coordinates or cached reference. Optionally provide an offset to click relative to the item.

```csharp
// Click by element selector
I.Click("#searchBox");

// Click by x, y coordinates
I.Click(10, 100);

// Click by relative offset from element selector
I.Click("#searchBox", 10, 100);

// Click using cached reference to element.
var element = I.Find("#searchBox");
I.Click(element);
```
```vbnet
// TODO - Visual Basic Code Sample
```