---
title: I.Open
link: Open
---
Open and navigate the web browser to the specified URL or <a href="http://msdn.microsoft.com/en-us/library/system.uri(v=vs.110).aspx" target="_blank">Uri</a>. Using a Uri can be valuable to validate your URI fragment before using it in a test.

```csharp
// Open browser via string/URL
I.Open("http://google.com");

// Open browser via URI
I.Open(new Uri("http://google.com"));
```
```vbnet
// TODO - Visual Basic Code Sample
```