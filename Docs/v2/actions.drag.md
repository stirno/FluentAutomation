---
title: I.Drag
link: Drag
---
Drag &amp; drop works with elements, coordinates and offsets. In the next version, the offset functionality will look a bit less... stupid. Sorry about that.

```csharp
// Drag one element to another
I.Drag("#drag").To("#drop");

// Drag one coordinate to another
I.Drag(100, 100).To(500, 500);

// Drag from element offset to another element
I.Drag(I.Find("#drag"), 50, 50).To("#drop");

// Drag from element to another elements offset
I.Drag("#drag").To(I.Find("#drop", 100, 30));
```
```vbnet
// TODO - Visual Basic Code Sample
```