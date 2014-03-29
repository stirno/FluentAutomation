---
title: Sticky Sessions
link: Sticky Sessions
---
**_Feature Status: Beta_**

One of those features built on an airplane (the best way to build new things, I think) -- Enables a set of tests to reuse the same browser instance between tests.

This can result in much faster execution times as compared to the standard method of creating a new browser instance for each test. Some Applications-under-test can experience intermittent issues in this configuration so it is not the default.

```csharp
// All tests executed after this will share browser instances
FluentSession.EnableStickySession();

// Stop sharing browser instances.
FluentSession.DisableStickySession();

// Pass the current tests session in and use it for other tests
// (in the scope of a test method)
FluentSession.SetStickySession(this.Session);
```
```vbnet
// TODO - Visual Basic Code Sample
```