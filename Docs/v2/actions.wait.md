---
title: I.Wait
link: Wait
---
Wait for a specified period of time before continuing the test. Method accepts a number of seconds or a <a href="http://msdn.microsoft.com/en-us/library/system.timespan(v=vs.110).aspx" target="_blank">TimeSpan</a>. Not guaranteed to be exact.

In most cases, your tests will be less fragile if you can utilize <a href="#i-waituntil">I.WaitUntil</a> instead.

```csharp
// Wait for 10 seconds
I.Wait(10);

// Wait for 500 milliseconds
I.Wait(TimeSpan.FromMilliseconds(500));
```
```vbnet
// TODO - Visual Basic Code Sample
```