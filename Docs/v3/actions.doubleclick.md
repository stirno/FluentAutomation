---
title: I.DoubleClick
link: DoubleClick
---
Double-click an element by selector, coordinates or cached reference. Optionally provide an offset to click relative to the item.

```csharp
// DoubleClick by element selector
I.DoubleClick("#searchBox");

// DoubleClick by x, y coordinates
I.DoubleClick(10, 100);

// DoubleClick by relative offset from element selector
I.DoubleClick("#searchBox", 10, 100);

// DoubleClick using cached reference to element.
var element = I.Find("#searchBox");
I.DoubleClick(element);
```
```vbnet
// TODO - Visual Basic Code Sample
```