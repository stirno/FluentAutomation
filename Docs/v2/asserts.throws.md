---
title: I.Assert.Throws
link: Throws
---
Assert that an `Exception` should be thrown by the anonymous function. Useful for negative assertions such as testing that something is not present.

```csharp
// Page has no errors
I.Assert.Throws(() => I.Assert.Exists(".error"));
```
```vbnet
// TODO - Visual Basic Code Sample
```