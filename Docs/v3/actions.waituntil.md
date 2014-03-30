---
title: I.WaitUntil
link: WaitUntil
---
Recommended method of waiting in tests. Conditional wait using anonymous functions that either return `true` / `false` or throw an `Exception` when the condition has not been met. Useful when content on the page is loaded dynamically or changes state during interactions.

If the condition has not been met, a timed wait will be executed before testing the condition again. The duration if this wait is set in `Settings.DefaultWaitUntilThreadSleep`.

The condition must succeed within the time set in `Settings.DefaultWaitUntilTimeout` or the test will fail.

Important Note: Most actions have implicit WaitUntil functionality built-in. Before adding, be sure your test needs it.

```csharp
// WaitUntil element exists
I.WaitUntil(() => I.Assert.Exists("#searchBar")));

// WaitUntil element has attribute 'data-loaded'
I.WaitUntil(() =>
    I.Find("#searchBar")()
        .Attributes
        .Get("data-loaded") == "true"
);
```
```vbnet
// TODO - Visual Basic Code Sample
```