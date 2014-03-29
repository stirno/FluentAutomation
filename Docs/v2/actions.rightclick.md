---
title: I.RightClick
link: RightClick
---
Right-click an element by selector, coordinates or cached reference. Optionally provide an offset to click relative to the item.

```csharp
// RightClick by element selector
I.RightClick("#searchBox");

// RightClick by x, y coordinates
I.RightClick(10, 100);

// RightClick by relative offset from element selector
I.RightClick("#searchBox", 10, 100);

// RightClick using cached reference to element.
var element = I.Find("#searchBox");
I.RightClick(element);
```
```vbnet
// TODO - Visual Basic Code Sample
```