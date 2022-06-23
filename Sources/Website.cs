using StatiCsharp.Interfaces;
using System.Globalization; // CultureInfo
using static System.Console;

namespace StatiCsharp
{
    /// <summary>
    /// Provides a website as a standalone object, able to render itself to all the files needed to upload onto a webserver.
    /// </summary>
    public partial class Website : IWebsite
    {
        /// <inheritdoc/>
        public string Url { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }

        /// <inheritdoc/>
        public CultureInfo Language { get; set; }

        /// <inheritdoc/>
        public string Output { get; set; }

        /// <inheritdoc/>
        public ISite Index { get; set; }

        /// <inheritdoc/>
        public List<IPage> Pages { get; set; }

        /// <inheritdoc/>
        public List<ISection> Sections { get; set; }

        /// <inheritdoc/>
        public List<string> MakeSectionsFor { get; set; }

        /// <inheritdoc/>
        public string Content { get; set; }

        /// <inheritdoc/>
        public string Resources { get; set; }

        /// <inheritdoc/>
        public string SourceDir { get; set; }

        /// <summary>
        /// List of all used paths while creating the sites.<br/>
        /// Used to find identical paths from meta data and to find files that have no markdown equivalent (got deleted) in GitMode.
        ///  </summary>
        private List<string> PathDirectory { get; set; }

        /// <summary>
        /// If true, the site generator only writes files if there are any changes.
        /// If false, all output files are rewritten.
        /// </summary>
        public bool GitMode { get; set; }

        /// <summary>
        /// Initialize a website.
        /// </summary>
        /// <param name="url">The complete domain of the website.</param>
        /// <param name="name">The name of the website.</param>
        /// <param name="description">A short description of the website. Is used for metadata in the html sites.</param>
        /// <param name="language">The language the websites main content is written in.</param>
        /// <param name="sections">Collection of the websites section-names. Folders in the content directory with names matching one item of this list a treated as sections.</param>
        /// <param name="source">The absolute path to the directory that contains the folders `content`, `output` and `resources`.</param>
        public Website(string url, string name, string description, string language, string sections, string source)
        {
            Url                 = url;
            Name                = name;
            Description         = description;
            Language            = new CultureInfo(language);
            SourceDir           = Path.Combine(source);
            Content             = Path.Combine(source, "Content");
            Resources           = Path.Combine(source, "Resources");
            Output              = Path.Combine(source, "Output");
            MakeSectionsFor     = sections.Replace(" ", string.Empty).Split(',').ToList();
            Index               = new Index();
            Pages               = new List<IPage>();
            Sections            = new List<ISection>();
            PathDirectory       = new List<string>();
            GitMode             = false;
        }

        /// <summary>
        /// Starts the generation of the website with the default theme.
        /// </summary>
        public void Make()
        {
            IHtmlFactory factory = new DefaultHtmlFactory();
            factory.WithWebsite(this);
            Make(factory);
        }

        /// <summary>
        /// Starts the generation of the website with the given theme.
        /// </summary>
        /// <param name="HtmlFactory"></param>
        public void Make(IHtmlFactory HtmlFactory)
        {
            if (!CheckEnvironment())
            {
                return;
            }

            HtmlFactory.WithWebsite(this);
            WriteLine("Making your website...");

            WriteLine("Collecting markdown data...");
            GenerateSitesFromMarkdown(this);
            
            if (!GitMode)
            {
                WriteLine("Deleting old output files...");
                DeleteAll(Output);
            }

            WriteLine("Generating index page...");
            MakeIndex(HtmlFactory);

            WriteLine("Generating pages...");
            MakePages(HtmlFactory);

            WriteLine("Generating sections...");
            MakeSections(HtmlFactory);

            WriteLine("Generating items...");
            MakeItems(HtmlFactory);

            WriteLine("Generating tag lists...");
            MakeTagLists(HtmlFactory);

            WriteLine("Copying resources...");
            CopyAll(Resources, Output);
            CopyAll(HtmlFactory.ResourcesPath, Output);

            WriteLine("Cleaning up...");
            CleanUp();

            WriteLine($"Success! Your website has been generated at {Output}");
            WriteLine("Press any key to exit...");
            ReadKey();
        }
    }
}