---
title: I.Hover
link: Hover
---
Cause the mouse to hover over a specified element, coordinates or position relative to an element.

```csharp
// Hover over element
I.Hover("#searchBox");

// Hover over x, y coordinates
I.Hover(10, 100);

// Hover over relative offset from element selector
I.Hover("#searchBox", 10, 100);

// Hover using cached reference to element.
var element = I.Find("#searchBox");
I.Hover(element);
```
```vbnet
// TODO - Visual Basic Code Sample
```