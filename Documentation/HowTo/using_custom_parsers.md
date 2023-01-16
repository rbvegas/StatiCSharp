# Using custom parsers

You can add your custom parsers to the generating process pipeline. StatiC# is distributed with a default markdown parser, using [Markdig](https://github.com/xoofx/markdig). This markdown parser runs at the end of every pipeline by default. If you add no additional parsers, [Markdig](https://github.com/xoofx/markdig) is the only one used.

## Add a parser

You can add every parser that implements `IPipelineParser` by calling the `AddParser()` method of the website manager. Configure your parser before the injection process.

```C#
var myParser = new CSharpToColouredHtmlParser();
manager.AddParser(myParser);

await manager.Make();
```

You can add as many parsers as you want, as the `AddParser()` method is chainable. The parsers run in the same order as they were added.

```C#
var myParser1 = new CSharpToColouredHtmlParser();
var myParser2 = new ReplacementParser();

manager
    .AddParser(myParser1)
    .AddParser(myParser2);

await manager.Make();
```

## Deactivate the default parser

If you use the `WebsiteManager` implementation of `IWebsiteManager` you can deactivate the default markdown parser by [Markdig](https://github.com/xoofx/markdig) by changing the corresponding property to `false`.

```C#
manager.UseDefaultMarkdownParser = false;
```