---
title: I.Select
link: Select
---

Primary method of selecting items in `<SELECT>` elements found via selector or cached reference. Selection can be done using `<OPTION>` value, text or index.

Selecting via Text/string will fall back to Value matching if no match is found. This simplifies the API for most users but may be confusing. If you need to guarantee the selection was based on value use the `Option` overload.

```csharp
// Select option by Text
I.Select("MN").From("#states");

// Select option by index
I.Select(12).From("#states");

// Select option by value
I.Select(Option.Value, 9999).From("#numbers");

// Select multiple options from a multiselect by text, index, or value
I.Select("ND", "MN").From("#states");
I.Select(10, 11, 12).From("#states");
I.Select(Option.Value, 9998, 9999).From("#numbers");
```
```vbnet
// TODO - Visual Basic Code Sample
```
