---
title: I.Assert.Url
link: Url
---
Assert that the browser has the specified Url. If using the string or Uri overloads, the match must be exact.

Supports anonymous functions that return `true` or `false`. Particularly useful on pages that modify the URL via hashtags or other mechanisms.

```csharp
// At #assert-url on docs
I.Assert.Url("http://fluent.stirno.com/docs/#assert-url");

// Verify we're on SSL
I.Assert.Url((uri) => uri.Scheme == "https");
```
```vbnet
// TODO - Visual Basic Code Sample
```