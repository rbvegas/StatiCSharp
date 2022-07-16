# Supported meta data for sites

You can provide meta data for all your sites (md-files).  
These meta data need to be placed at the top of your markdown files and is [yaml](https://en.wikipedia.org/wiki/YAML) like. You find an example in the [content template](https://github.com/rolandbraun-dev/StatiCSharp/blob/master/Documentation/HowTo/content-template.md).  

It is recommended to provide all meta data in your files.  

The folling entries are currently available:

- Title: The title of that site.
- Description: A short description of the site. Is displayed in the itemlist e.g.
- Author: The authors name.
- Date: The date this site was created by ISO 8601. E.g. 2022-06-26 . Times are not supported.
- Path: The path the site is available at, relatively to its hierachy. If no path is provided, the filename is used.
- Tags: The tags that the site is corresponding to, seperated by comma. E.g. tag1, tag2, tag3

Important! => To mark your entries as meta data, make sure the data is written between "---". The first "---" need to be placed in the first line of the document!

```
---
Title: The title of that site.
Description: A short description of the site. Is displayed in the itemlist e.g.
Author: The authors name.
Date: The date this site was created by ISO 8601. E.g. 2022-06-26 . Times are not supported.
Path: The path the site is available at, relatively to its hierachy. If no path is provided, the filename is used.
Tags: The tags that the site is corresponding to, seperated by comma. E.g. tag1, tag2, tag3
---
```
