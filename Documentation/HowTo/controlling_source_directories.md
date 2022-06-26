# Controlling Source Directories
 
 After initializing a new Website object the default location for your output, content and static files is in the given source directory. E.g.
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
 
If you want to change this behavior you can change these defaults by changing the corresponding properties of the Website object:

```C#
website.Content   = @"another\path\to\content";
website.Output    = @"another\path\to\output";
website.Resources = @"another\path\to\resources";
```
