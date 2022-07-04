# Controlling Source Directories
 
 After initializing a new WebsiteManager the default location for your output, content and static files is in the given source directory. E.g.
 ```C#
 ...
 source: @"path\to\myWebsite"
 ...
 ```
 and StatiC# will assume this folder structure:
 
 ```bash
 ├── myWebsite
 │   ├── ...
 │   ├── Content
 │   ├── Output
 │   ├── Resources
 │   ├── ...
 ```
 
If you want to change this behavior you can change these defaults by changing the corresponding properties of the WebsiteManager:

```C#
manager.Content   = @"another\path\to\Content";
manager.Output    = @"another\path\to\Output";
manager.Resources = @"another\path\to\Resources";
```
