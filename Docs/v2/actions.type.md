---
title: I.Type
link: Type
---
Type a string, one character at a time using OS level keypress events. This functionality will send keypress events to whatever the active window is at the time its trigger, currently **not guaranteed to be the actual browser window. Use with caution.**

The intended use is for interactive with elements that steal focus or are not a part of the DOM such as Flash.

Type does not support the use of special key values.

```csharp
// Type string
I.Type("FluentAutomation");
```
```vbnet
// TODO - Visual Basic Code Sample
```