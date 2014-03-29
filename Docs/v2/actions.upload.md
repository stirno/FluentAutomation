---
title: I.Upload
link: Upload
---
Upload a file using an `<input type="file">` on the current page. The provided path must be absolute and point to the file you want to upload.

This has been used with several Flash uploaders without issue. Your mileage may vary.

```csharp
// Upload LoginScreen.jpg
I.Upload("input[type='file'].uploader", @"C:\LoginScreen");
```
```vbnet
// TODO - Visual Basic Code Sample
```