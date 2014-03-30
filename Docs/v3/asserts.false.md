---
title: I.Assert.False
link: 'False'
---
Assert that an anonymous function should return false. Use with `I.Find` to fail tests properly if conditions are not met.

```csharp
// Element is not a select box
var element = I.Find("input");
I.Assert.False(() => element().IsSelect);
```
```vbnet
// TODO - Visual Basic Code Sample
```