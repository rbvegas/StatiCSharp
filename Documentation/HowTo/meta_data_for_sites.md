# Supported metadata for sites

You can provide metadata for all your sites (md-files).  
These metadata need to be placed at the top of your markdown files and are [yaml](https://en.wikipedia.org/wiki/YAML) like. You find an example in the [content template](https://github.com/rolandbraun-dev/StatiCSharp/blob/master/Documentation/HowTo/content-template.md).  

It is recommended to provide all metadata in your files.  

The following entries are currently available:

- Published: A boolean value of whether the site is ready to be published. If false, no HTML file is generated. True is the default.
- Title: The title of that site.
- Description: A short description of the site. Is displayed in the item list e.g.
- Author: The authors' name.
- Date: The date this site was created by ISO 8601. E.g., 2022-06-26 Times are not supported.
- Path: The path the site is available at relative to its hierarchy. If no path is provided, the filename is used.
- Tags: The tags the site corresponds to, separated by a comma. E.g. tag1, tag2, tag3

Important! => To mark your entries as metadata, ensure the data is written between "---". The first "---" need to be placed in the document's first line!

```
---
Published: true
Title: The title of that site.
Description: A short description of the site. Is displayed in the item list e.g.
Author: The authors' name.
Date: The date this site was created by ISO 8601. E.g., 2022-06-26 Times are not supported.
Path: The path the site is available at relative to its hierarchy. If no path is provided, the filename is used.
Tags: The tags the site corresponds to, separated by a comma. E.g. tag1, tag2, tag3
---
```