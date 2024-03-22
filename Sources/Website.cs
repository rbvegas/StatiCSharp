using StatiCSharp.Builder;
using StatiCSharp.Interfaces;
using System.Collections.Generic;
using System.Globalization; // CultureInfo
using static System.Console;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using StatiCSharp.Tools;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace StatiCSharp;

/// <summary>
/// Provides a website as a data object.
/// </summary>
public partial class StaticWebApplication : IWebsite
{
    private string _sourcePath;
    private IServiceProvider _servicesProvider;
    private HtmlBuilder _htmlBuilder = new HtmlBuilder(useDefaultMarkdownParser: true);
    private IFileSystemService _fileSystemService;
    private IEnvironmentService _environmentService;
    private ISiteGenerationService _siteGenerationService;
    private IMetaDataService _metaDataService;
    private IHtmlService _htmlService;

    /// <inheritdoc/>
    public string Url { get; set; }

    /// <inheritdoc/>
    public string Name { get; set; }

    /// <inheritdoc/>
    public string Description { get; set; }

    /// <inheritdoc/>
    public CultureInfo Language { get; set; }

    /// <inheritdoc/>
    public IIndex Index { get; set; } = new Index();

    /// <inheritdoc/>
    public List<IPage> Pages { get; set; } = new List<IPage>();

    /// <inheritdoc/>
    public List<ISection> Sections { get; set; } = new List<ISection>();

    /// <inheritdoc/>
    public List<string> MakeSectionsFor { get; set; }

    /// <inheritdoc/>
    public string Content { get; set; }

    /// <inheritdoc/>
    public string Resources { get; set; }

    /// <inheritdoc/>
    public string Output { get; set; }

    /// <inheritdoc/>
    public IHtmlFactory HtmlFactory { get; set; }

    /// <inheritdoc/>
    public bool GitMode { get; private set; } = false;

    /// <summary>
    /// List of all used paths while creating the sites.<br/>
    /// Used to find identical paths from meta data and to find files that have no markdown equivalent (got deleted) in GitMode.
    ///  </summary>
    private List<string> PathDirectory { get; set; }


    /// <summary>
    /// Initialize a website.
    /// </summary>
    /// <param name="url">The complete domain of the website.</param>
    /// <param name="name">The name of the website.</param>
    /// <param name="description">A short description of the website. Is used for metadata in the html sites.</param>
    /// <param name="language">The language the websites main content is written in.</param>
    /// <param name="sections">Collection of the websites section-names. Folders in the content directory with names matching one item of this list a treated as sections.</param>
    public StaticWebApplication(string url, string name, string description, string language, string sections, IServiceProvider serviceProvider)
        : this(url, name, description, new CultureInfo(language), sections.Replace(" ", string.Empty).Split(',').ToList(), serviceProvider)
    {
    }

    /// <summary>
    /// Initialize a website.
    /// </summary>
    /// <param name="url">The complete domain of the website.</param>
    /// <param name="name">The name of the website.</param>
    /// <param name="description">A short description of the website. Is used for metadata in the html sites.</param>
    /// <param name="language">The language the websites main content is written in.</param>
    /// <param name="sections">Collection of the websites section-names. Folders in the content directory with names matching one item of this list a treated as sections.</param>
    public StaticWebApplication(string url, string name, string description, CultureInfo language, List<string> sections, IServiceProvider serviceProvider)
    {
        Url                 = url;
        Name                = name;
        Description         = description;
        Language            = language;
        MakeSectionsFor     = sections;
        _servicesProvider   = serviceProvider;
        InitializeServicesFromServiceProvider();
    }

    /// <summary>
    /// Initializes a new instance of the StaticWebApplicationBuilder class with preconfigured defaults.
    /// </summary>
    /// <returns></returns>
    public static StaticWebApplicationBuilder CreateBuilder()
    {
        return new StaticWebApplicationBuilder();
    }

    public void AddSource(string path)
    {
        _sourcePath = path;
    }

    /// <summary>
    /// Starts the generating process of the website with the given theme.<br/>
    /// Files are saved in the `Output` directory.<br/><br/>
    /// If the `Output` directory is not explicity set, it is inside the `Source` directory.
    /// </summary>
    public async Task Make()
    {
        WriteLine("Website generating process startet...");

        WriteLine("Checking environment...");
        var checkEnvTask = Task.Run(() => _environmentService.CheckEnvironment(Output, Content, Resources, HtmlFactory.ResourcesPath)).ConfigureAwait(false);
        await checkEnvTask;

        WriteLine("Collecting markdown data...");
        await _siteGenerationService.GenerateSitesFromMarkdown(this, _htmlBuilder, _metaDataService);

        if (!GitMode)
        {
            WriteLine("Deleting old output files...");
            var deleteAllTask = _fileSystemService.DeleteAllAsync(Output);
            await deleteAllTask;
        }

        WriteLine("Copying theme resources...");
        await _fileSystemService.CopyAllAsync(HtmlFactory.ResourcesPath, Output);

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

        WriteLine("Cleaning up...");
        await CleanUpAsync();

        WriteLine("Copying user resources...");
        await _fileSystemService.CopyAllAsync(Resources, Output);

        WriteLine($"Success! Your website has been generated at {Output}");
    }

    /// <summary>
    /// Asynchronous creates and writes the index (homepage) of the website.
    /// </summary>
    /// <returns>A <see cref="Task"/> that represents the asynchronous index generating operation.</returns>
    private async Task MakeIndexAsync()
    {
        string body = HtmlFactory.MakeIndexHtml(Index);
        string head = HtmlFactory.MakeHeadHtml();
        string index = _htmlService.AddLeadingHtmlCode(this, Index, head, body, _htmlBuilder);
        await _fileSystemService.WriteFileAsync(Output, "index.html", index, gitMode: GitMode);
        PathDirectory.Add(Output);
    }

    /// <summary>
    /// Asynchronous creates and writes the pages (not sections or items) of the website.
    /// </summary>
    /// <returns>A <see cref="Task"/> that represents the asynchronous pages generating operation.</returns>
    private async Task MakePagesAsync()
    {
        List<Task> tasks = new List<Task>();

        foreach (IPage site in Pages)
        {
            tasks.Add(WritePage(site));
        }

        await Task.WhenAll(tasks);

        async Task WritePage(IPage site)
        {
            string body = HtmlFactory.MakePageHtml(site);
            string head = HtmlFactory.MakeHeadHtml();
            string page = _htmlService.AddLeadingHtmlCode(this, site, head, body, _htmlBuilder);
            string defaultPath = FilenameToPath.From(site.MarkdownFileName);

            // Create directory, if it does not excist.
            string pathInHierachy = (site.Path == string.Empty) ? defaultPath : site.Path;
            if (pathInHierachy == "index") { pathInHierachy = string.Empty; }
            string path = Directory.CreateDirectory(Path.Combine(Output, site.Hierarchy, pathInHierachy)).ToString();

            if (PathDirectory.Contains(path))
            {
                WriteLine($"WARNING: The path {path} is allready in use. Change the path in meta data to avoid duplicates.");
            }

            await _fileSystemService.WriteFileAsync(path: path, filename: "index.html", content: page, gitMode: GitMode);
            PathDirectory.Add(path);
        }
    }

    /// <summary>
    /// Asynchronously creates and writes the sections (not pages or items) of the website.
    /// </summary>
    /// <returns>A <see cref="Task"/> that represents the asynchronous sections generating operation.</returns>
    private async Task MakeSectionsAsync()
    {
        List<Task> tasks = new List<Task>();

        foreach (ISection site in Sections)
        {
            tasks.Add(WriteSection(site));
        }

        await Task.WhenAll(tasks);

        async Task WriteSection(ISection site)
        {
            string body = HtmlFactory.MakeSectionHtml(site);
            string head = HtmlFactory.MakeHeadHtml();
            string page = _htmlService.AddLeadingHtmlCode(this, site, head, body, _htmlBuilder);
            string path = Directory.CreateDirectory(Path.Combine(Output, site.SectionName)).ToString();

            if (PathDirectory.Contains(path))
            {
                WriteLine($"WARNING: The path {path} is allready in use. Change the path in meta data to avoid duplicates.");
            }

            await _fileSystemService.WriteFileAsync(path: path, filename: "index.html", content: page, gitMode: GitMode);

            PathDirectory.Add(path);
        }
    }

    /// <summary>
    /// Asynchronous creates and writes the items (not sections or pages) of the website.
    /// </summary>
    /// <returns>A <see cref="Task"/> that represents the asynchronous items generating operation.</returns>
    private async Task MakeItemsAsync()
    {
        List<Task> tasks = new List<Task>();

        foreach (ISection section in Sections)
        {
            foreach(IItem site in section.Items)
            {
                tasks.Add(WriteItem(section, site));
            }
        }

        await Task.WhenAll(tasks);

        async Task WriteItem(ISection section, IItem site)
        {
            string body = HtmlFactory.MakeItemHtml(site);
            string head = HtmlFactory.MakeHeadHtml();
            string page = _htmlService.AddLeadingHtmlCode(this, site, head, body, _htmlBuilder);
            string defaultPath = FilenameToPath.From(site.MarkdownFileName);

            string itemPath = (site.Path != string.Empty) ? site.Path : defaultPath;
            string path = Directory.CreateDirectory(Path.Combine(Output, section.SectionName, itemPath)).ToString();

            if (PathDirectory.Contains(path))
            {
                WriteLine($"WARNING: The path {path} is allready in use. Change the path in meta data to avoid duplicates.");
            }

            await _fileSystemService.WriteFileAsync(path: path, filename: "index.html", content: page, gitMode: GitMode);

            PathDirectory.Add(path);
        }
    }

    /// <summary>
    /// Asynchronously creates and writes the tags pages of the website.
    /// </summary>
    /// <returns>A <see cref="Task"/> that represents the asynchronous tags generating operation.</returns>
    private async Task MakeTagListsAsync()
    {
        // Collect all available tags
        List<string> tags = new List<string>();
        foreach (ISection currentSection in Sections)
        {
            foreach (IItem currentItem in currentSection.Items)
            {
                foreach (string tag in currentItem.Tags)
                {
                    // Check if tag is already in list. If not, add it
                    if (!tags.Contains(tag)) { tags.Add(tag); }
                }
            }
        }

        List<Task> tasks = new List<Task>();
        
        foreach (string tag in tags)
        {
            tasks.Add(WriteTagList(tag));
        }

        await Task.WhenAll(tasks);

        async Task WriteTagList(string tag)
        {
            List<IItem> itemsWithCurrentTag = new();
            // Collect all items with the current tag
            foreach (ISection currentSection in Sections)
            {
                foreach (IItem item in currentSection.Items)
                {
                    if (item.Tags.Contains(tag))
                    {
                        itemsWithCurrentTag.Add(item);
                    }
                }
            }

            // Write tags sites to files
            IItem tagPage = new Item();
            tagPage.Title = $"{tag} | {Name}";
            string body = HtmlFactory.MakeTagListHtml(itemsWithCurrentTag, tag);
            string head = HtmlFactory.MakeHeadHtml();
            string page = _htmlService.AddLeadingHtmlCode(this, tagPage, head, body, _htmlBuilder);

            // Create directory, if it does not excist
            string path = Directory.CreateDirectory(Path.Combine(Output, "tag", tag)).ToString();

            if (PathDirectory.Contains(path))
            {
                WriteLine($"WARNING: The path {path} is allready in use. Change the path in meta data to avoid duplicates.");
            }

            await _fileSystemService.WriteFileAsync(path: path, filename: "index.html", content: page, gitMode: GitMode);

            PathDirectory.Add(path);
        }
    }

    /// <summary>
    /// Cleans up the Output directory from HTML files that have no corresponding markdown file.
    /// </summary>
    /// <returns>A <see cref="Task"/> that represents the asynchronous clean up operation.</returns>
    private async Task CleanUpAsync()
    {
        await cleanUpDirectory(Output);


        async Task cleanUpDirectory(string directory)
        {
            if (!PathDirectory.Contains(directory))
            {
                // Delete only files named index.html. Other files could be resources!
                if (File.Exists(Path.Combine(directory, "index.html")))
                {
                    File.Delete(Path.Combine(directory, "index.html"));
                }
            }
            if (Directory.GetDirectories(directory).Length == 0 && Directory.GetFiles(directory).Length == 0)
            {
                // Do not delete output directory!
                if (directory != Output)
                {
                    Directory.Delete(directory);
                }
            }
            else
            {
                foreach (string subdir in Directory.GetDirectories(directory))
                {
                    await cleanUpDirectory(subdir);
                }
            }
            
        }
    }

    private void InitializeServicesFromServiceProvider()
    {
        _fileSystemService = _servicesProvider.GetRequiredService<IFileSystemService>();
        _environmentService = _servicesProvider.GetRequiredService<IEnvironmentService>();
        _siteGenerationService = _servicesProvider.GetRequiredService<ISiteGenerationService>();
        _metaDataService = _servicesProvider.GetRequiredService<IMetaDataService>();
        _htmlService = _servicesProvider.GetRequiredService<IHtmlService>();
    }
}