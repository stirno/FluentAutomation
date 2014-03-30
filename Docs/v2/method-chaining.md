---
title: Method Chaining
link: Method Chaining
---
As part of trying to provide the best fluent syntax we can, we now have method chaining on all "terminating methods". These are the bits that actually execute or do something, rather than just configure the next step.

This provides a pretty clean, very simple way to push your tests towards AAA (Arrange/Act/Assert). As you can see to the right, clean and tight code.

```csharp
I.Open("http://automation.apphb.com/forms")
    .Select("Motorcycles").From(".liveExample tr select:eq(0)")
    .Select(2).From(".liveExample tr select:eq(1)")
    .Enter(6).In(".liveExample td.quantity input:eq(0)")
    .Expect
        .Text("$197.72").In(".liveExample tr span:eq(1)")
        .Value(6).In(".liveExample td.quantity input:eq(0)");
```
```vbnet
// TODO - Visual Basic Code Sample
```