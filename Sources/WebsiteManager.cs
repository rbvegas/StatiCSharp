using StatiCsharp.Interfaces;
using static System.Console;

namespace StatiCsharp
{
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
        public void Make()
        {
            WriteLine("Checking environment...");
            if (!CheckEnvironment())
            {
                return;
            }

            WriteLine("Starting generating your website:");

            WriteLine("Collecting markdown data...");
            GenerateSitesFromMarkdown();

            if (!GitMode)
            {
                WriteLine("Deleting old output files...");
                DeleteAll(Output);
            }

            WriteLine("Copying theme resources...");
            CopyAll(HtmlFactory.ResourcesPath, Output);

            WriteLine("Writing index...");
            MakeIndex();

            WriteLine("Writing pages...");
            MakePages();

            WriteLine("Writing sections...");
            MakeSections();

            WriteLine("Writing items...");
            MakeItems();

            WriteLine("Writing tag lists...");
            MakeTagLists();

            WriteLine("Copying user resources...");
            CopyAll(Resources, Output);

            WriteLine("Cleaning up...");
            CleanUp();

            WriteLine($"Success! Your website has been generated at {Output}");
        }
    }
}
