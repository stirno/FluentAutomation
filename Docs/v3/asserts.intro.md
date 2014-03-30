---
title: Expect/Assert
link: Expect/Assert
---
As of `v2.3` proper assertions were added to FluentAutomation.

Fundamentally `Expect` and `Assert` work exactly the same. The methods and their signatures are identical. The difference is that, when `Settings.ExpectIsAssert` is set to `false`, `Assert` will `throw` an `Exception` and fail a test. In comparison, `Expect` will attempt to log the failure using the `Action` provided in `Settings.ExpectFailedCallback` and allow the test to continue

For ease of maintaining the documentation, we will document `Assert` methods only. Unless otherwise noted, `Expect` methods are identical.

**Warning:** The current default value of `Settings.ExpectIsAssert` is `true`. This will change in `v2.4`. If you want to guarantee your test behavior doesn't change, set the value in your project before upgrading.