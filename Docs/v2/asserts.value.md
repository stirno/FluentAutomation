---
title: I.Assert.Value
link: Value
---
Assert that an element matching selector has the specified value. Works with `<INPUT>`, `<TEXTAREA>` and `<SELECT>`

Supports anonymous functions that return `true` or `false`.

```csharp
// Dropdown has value of 10.
I.Assert.Value(10).In("#quantity");

// Value starts with 'M'
I.Assert.Value((value) => value.StartsWith("M")).In("#states");
```
```vbnet
// TODO - Visual Basic Code Sample
```