<p align="center">
    <img src="Logo.png" width="400" max-width="90%" alt="StatiC#" />
</p>

<p align="center">
    <a href="https://docs.microsoft.com/en-us/dotnet/csharp/">
        <img src="https://img.shields.io/badge/C%23-10.0-blue?style=flat" alt="C# 10.0" />
    </a>
    <a href="https://dotnet.microsoft.com">
        <img src="https://img.shields.io/badge/.NET-6.0-blueviolet?style=flat" />
    </a>
    <img src="https://img.shields.io/badge/Platforms-Win+Mac+Linux-green?style=flat" />
    <img src="https://img.shields.io/badge/Version-0.1-green?style=flat" />
</p>

Welcome to **StatiC#**, a static webside generator written in C#. It enables entire websites to be built using C#. Custom themes can be used by editing the integrated default theme or by importing a theme.

---

StatiC# provides a website as a standalone object, able to render itself to all the files needed to upload onto a webserver.  

If you want to quickstart with you new website, you can start with the default configuration and build up from there. Here is an example:

```C#
using StatiCsharp;

Website myAwesomeWebsite = new Website(
    url: "https://yourdomain.com",
    name: "My Awesome Website",
    description: @"Description of your website",
    language: "en-US",
    sections: new List<string>() { "posts", "about" },
    source: @"/path/to/your/content"
    );

myAwesomeWebsite.Make();
```
