---
title: I.Press
link: Press
---
Triggers a single OS level keypress event. This method will send events to whatever the active window is at the time its trigger, currently **not guaranteed to be the actual browser window. Use with caution.**

The intended use is for interactive with elements that steal focus or are not a part of the DOM such as Flash.

Several keys require special values to be used. Refer to the <a href="http://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys.aspx" target="_blank">Windows Forms SendKeys documentation</a> for valid values.

```csharp
// Press Tab key
I.Press("{TAB}");
```
```vbnet
// TODO - Visual Basic Code Sample
```