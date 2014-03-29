---
title: I.Assert.True
link: 'True'
---
Assert that an anonymous function should return true. Use with `I.Find` to fail tests properly if conditions are not met.

```csharp
// Element is a select box
var element = I.Find("select");
I.Assert.True(() => element().IsSelect);
```
```vbnet
// TODO - Visual Basic Code Sample
```