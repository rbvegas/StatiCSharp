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
    <img src="https://img.shields.io/badge/Version-0.1.0--alpha2-green?style=flat" />
</p>

Welcome to **StatiC#**, a static webside generator written in C#. It enables entire websites to be built using C#. Custom themes can be used by editing the integrated default theme or by importing a theme.

---

StatiC# provides a website as a standalone object, able to render itself to all the files needed to upload onto a webserver.  

If you want to quickstart with your new website, you can start with the default configuration and build up from there. Here is an example:

```C#
using StatiCsharp;

var myAwesomeWebsite = new Website(
    url: "https://yourdomain.com",
    name: "My Awesome Website",
    description: @"Description of your website",
    language: "en-US",
    sections: "posts, about",
    source: @"/path/to/your/content"
    );

myAwesomeWebsite.Make();
```

## Installation

### Download StatiC#

In the current version StatiC# is only available from the project files. Download the project files to your local machine via git is recommended. To do so create a new folder at a place of your choice and open a terminal window at this location. Clone StatiC# to your machine by entering:

```
$ git init
$ git clone https://github.com/rolandbraun-dev/StatiCsharp.git
```

That's all. For the rest we can leave this folder untouched.

### Add StatiC# to your project

To get started, create a new console application at a path of your choice. Let's say that your new website is called *myWebsite*:

```
$ dotnet new console -n myWebsite
```
After .NET has created the project files we can add StatiC# as a project reference. Open `myWebsite.csproj` and add the path of the previous download of StatiC#. The file should then look something like this:

```
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net6.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\path\to\StatiCsharp\StatiCsharp.csproj" />
  </ItemGroup>

</Project>
```

Then you can get started using StatiC# by importing it at the beginning of your `Program.cs`:

```C#
using StatiCsharp;
```

## Quick start

StatiC# expects three folders to work with at the path given during the initialization of our website (we will come to that later).  
  
`content`: This folder contains the markdown files that our website depents on.  
`output`: Here the final website with all the necessary files will be saved.  
`resources`: Put all your static files in here. All files will be copied, without any manipulation, to the output. Folders are migrated.  

I recomment to put those folders within your project folder of *myWebsite*.  Also copy `styles.css` from the StatiC# project-folder to this folder (if you want to use the default theme, what I also recomment to get started). Your folder should look something like this:

```bash
├── myWebsite
│   ├── content
│   ├── obj
│   ├── outout
│   ├── resources
│   ├── myWebsite.csproj
│   ├── Program.cs
│   ├── styles.css
```
StatiC# renders four different types of sites:  

*index*: The homepage of your website  
*pages*: Normal sites e.g. your about page.  
*sections*: Sites that contain items e.g. articles in a specifig field.  
*items*: The sites that are part of a section.  
  
Add some content to your website by adding your markdown files to the `content` folder. Check out the [documentation](/Documentation) for a [template file](Documentation/HowTo/content-template.md):

```bash
├── myWebsite
│   ├── content
│   │   ├── index.md                    # this is your homepage 
│   │   ├── posts                       # contains all items for the posts section
│   │   │   ├── index.md                # content of the section site
│   │   │   ├── your-first-post.md      # item within the posts section
│   │   │   ├── your-second-post.md     # another item within the posts section
│   │   ├── about                       # contains a page
│   │   │   ├── index.md                # content of the about page
│   │   │   ├── another-page.md         # content of another page
│   ├── obj
│   ├── outout
│   ├── resources
│   ├── myWebsite.csproj
│   ├── Program.cs
```

Store your content in folders and StatiC# cares about the rest. All folders are treated as pages unless their name is used to build a section.  

Finally set up the parameters in `Program.cs` in your *myWebsite* project:

```C#
using StatiCsharp;

var myAwesomeWebsite = new Website(
    url: "https://yourdomain.com",
    name: "My Awesome Website",
    description: @"Description of your website",
    language: "en-US",
    sections: "posts, about",         //select which folders should be treated as sections
    source: @"/path/to/myWebsite"     // path to the folder of your website
    );

myAwesomeWebsite.Make();
```

Run the project and your new awesome website will be generated in the `output` directory:
```
$ dotnet run
```

## Contributions and support

StatiC# is developed completely open, and your contributions are more than welcome.

Before you start using StatiC# in any of your projects, please have in mind that it’s a hobby project and there is no guarantee for technical correctness or future releases.  

Since this is a very young project, it’s likely to have many limitations and missing features, which is something that can really only be discovered and addressed as you use it. While StatiC# is used in production on my personal website, it’s recommended that you first try it out for your specific use case, to make sure it supports the features that you need.  

If you wish to make a change, [open a Pull Request](https://github.com/rolandbraun-dev/StatiCsharp/pull/new) — even if it just contains a draft of the changes you’re planning, or a test that reproduces an issue — and we can discuss it further from there.

I hope you’ll enjoy using StatiC#!


## Future Features

⬜ Syntax highlighting  
⬜ Math  
⬜ Build in markdown parser