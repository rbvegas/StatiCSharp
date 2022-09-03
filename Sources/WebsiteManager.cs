using StatiCSharp.Interfaces;
using System.Diagnostics;
using static System.Console;

namespace StatiCSharp;

/// <summary>
/// Manager that handles the website generating process with the given website and theme.
/// </summary>
public partial class WebsiteManager : IWebsiteManager
{
    /// <inheritdoc/>
    public string SourceDir { get; set; }

    /// <inheritdoc/>
    public string Content { get; set; }

    /// <inheritdoc/>
    public string Resources { get; set; }

    /// <inheritdoc/>
    public string Output { get; set; }

    /// <inheritdoc/>
    public bool GitMode { get; set; }

    /// <summary>
    /// List of all used paths while creating the sites.<br/>
    /// Used to find identical paths from meta data and to find files that have no markdown equivalent (got deleted) in GitMode.
    ///  </summary>
    private List<string> PathDirectory { get; set; }

    /// <inheritdoc/>
    public IWebsite Website { get; set; }

    /// <inheritdoc/>
    public IHtmlFactory HtmlFactory { get; set; }

    /// <summary>
    /// Initialize a new manager that generates the output from a given website and theme.
    /// </summary>
    /// <param name="website">The website that contains the content.</param>
    /// <param name="htmlFactory">The theme for the website.</param>
    /// <param name="source">The absolute path to the directory that contains the folders `Content`, `Output` and `Resources`.</param>
    public WebsiteManager(IWebsite website, IHtmlFactory htmlFactory, string source)
    {
        Website         = website;
        HtmlFactory     = htmlFactory;
        SourceDir       = Path.Combine(source);
        Content         = Path.Combine(source, "Content");
        Resources       = Path.Combine(source, "Resources");
        Output          = Path.Combine(source, "Output");
        GitMode         = false;
        PathDirectory   = new List<string>();
    }

    /// <summary>
    /// Initialize a new manager that generates the output from a given website and theme.
    /// </summary>
    /// <param name="website">The website that contains the content.</param>
    /// <param name="source">The absolute path to the directory that contains the folders `Content`, `Output` and `Resources`.</param>
    public WebsiteManager(IWebsite website, string source)
    {
        Website         = website;
        HtmlFactory     = new DefaultHtmlFactory(Website);
        SourceDir       = Path.Combine(source);
        Content         = Path.Combine(source, "Content");
        Resources       = Path.Combine(source, "Resources");
        Output          = Path.Combine(source, "Output");
        GitMode         = false;
        PathDirectory   = new List<string>();
    }

    /// <inheritdoc/>
    public async Task Make()
    {
        WriteLine("Checking environment...");
        await CheckEnvironmentAsync(HtmlFactory.ResourcesPath);

        WriteLine("Starting generating your website:");

        WriteLine("Collecting markdown data...");
        await GenerateSitesFromMarkdownAsync();

        if (!GitMode)
        {
            WriteLine("Deleting old output files...");
            await DeleteAllAsync(Output);
        }

        WriteLine("Copying theme resources...");
        CopyAll(HtmlFactory.ResourcesPath, Output);

        WriteLine("Writing index...");
        await MakeIndexAsync();

        WriteLine("Writing pages...");
        await MakePagesAsync();

        WriteLine("Writing sections...");
        await MakeSectionsAsync();

        WriteLine("Writing items...");
        await MakeItemsAsync();

        WriteLine("Writing tag lists...");
        await MakeTagListsAsync();

        WriteLine("Copying user resources...");
        await CopyAllAsync(Resources, Output);

        WriteLine("Cleaning up...");
        await CleanUpAsync();

        WriteLine($"Success! Your website has been generated at {Output}");
    }
}
