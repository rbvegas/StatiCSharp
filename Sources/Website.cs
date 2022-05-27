using StatiCsharp.Interfaces;
using System.Globalization; // CultureInfo
using static System.Console;

namespace StatiCsharp
{
    public partial class Website : IWebsite
    {
        private string _url;
        /// <summary>
        /// The domain of the website.E.g. "https://mydomain.com".
        /// </summary>
        public string Url
        {
            get { return _url; }
        }

        private string _name;
        /// <summary>
        /// The name of the website.
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        private string _description;
        /// <summary>
        /// A short description of the website. Is used for metadata in the html sites.
        /// </summary>
        public string Description
        {
            get { return _description; }
        }

        private CultureInfo _language;
        /// <summary>
        /// The language the websites main content is written in.
        /// </summary>
        public CultureInfo Language
        {
            get { return _language; }
        }

        private string _output;
        /// <summary>
        /// The absolute path to the output directory.
        /// </summary>
        public string Output
        {
            get { return _output; }
        }

        private ISite _index = new Index();
        /// <summary>
        /// The index/homepage of the website.
        /// </summary>
        public ISite Index { get { return this._index; } }

        private List<ISite> _pages = new List<ISite>();
        /// <summary>
        /// The collection of pages the website contains.
        /// </summary>
        public List<ISite> Pages
        {
            get { return this._pages; }
            set { this._pages = value; }
        }

        private List<ISection> _sections = new List<ISection>();
        /// <summary>
        /// The collection of sections the website contains.
        /// </summary>
        public List<ISection> Sections
        {
            get { return this._sections; }
            set { this._sections = value; }
        }

        private List<string> _makeSectionsFor = new List<string>();
        /// <summary>
        /// Collection of the websites section-names. Folders in the content directory with names matching one item of this list a treated as sections.
        /// </summary>
        public List<string> MakeSectionsFor
        {
            get { return this._makeSectionsFor; }
            set { this._makeSectionsFor = value; }
        }

        private string _content;
        /// <summary>
        /// The abolute path to the content (markdown-files) for the website.
        /// </summary>
        public string Content
        {
            get { return this._content; }
        }

        private string _resources = string.Empty;
        /// <summary>
        /// The absolute path to the resources directory for the website.
        /// </summary>
        public string Resources
        { 
            get { return this._resources;} 
            set { this._resources = value; }
        }

        private string _sourceDir;
        /// <summary>
        /// The absolute path to the directory that contains the folders `content`, `output` and `resources`.
        /// </summary>
        public string SourceDir
        {
            get { return this._sourceDir;}
            set { this._sourceDir = value; }
        }

        private List<string> _pathDirectory = new List<string>();
        /// <summary>
        /// List of all used paths while creating the sites.
        /// </summary>
        public List<string> PathDirectory
        {
            get { return this._pathDirectory; }
            set { this._pathDirectory = value; }
        }

        private bool _gitMode = false;
        /// <summary>
        /// If true, the site generator only writes files if there are any changes.
        /// If false, all output files are rewritten.
        /// </summary>
        public bool GitMode
        {
            get { return this._gitMode; }
            set { this._gitMode = value; }
        }

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
            this._url = url;
            this._name = name;
            this._description = description;
            this._language = new CultureInfo(language);
            this._sourceDir = Path.Combine(source);
            this._content = Path.Combine(source, "content");
            this._resources = Path.Combine(source, "resources");
            this._output = Path.Combine(source, "output");
            this._makeSectionsFor = sections.Replace(" ", string.Empty).Split(',').ToList();
        }

        /// <summary>
        /// Starts the generation of the website with the default theme.
        /// </summary>
        public void Make()
        {
            IHtmlFactory factory = new DefaultHtmlFactory();
            factory.WithWebsite(this);
            this.Make(factory);
        }

        /// <summary>
        /// Starts the generation of the website with the given theme.
        /// </summary>
        /// <param name="HtmlFactory"></param>
        public void Make(IHtmlFactory HtmlFactory)
        {
            HtmlFactory.WithWebsite(this);
            WriteLine("Making your website...");

            WriteLine("Collecting markdown data...");
            GenerateSitesFromMarkdown(this);
            
            if (!this._gitMode)
            {
                WriteLine("Deleting old output files...");
                DeleteAll(this._output);
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
            CopyAll(this.Resources, _output);
            File.Copy(HtmlFactory.ResourcePaths, Path.Combine(_output, "styles.css"), true);

            WriteLine("Cleaning up...");
            CleanUp();
        }
    }
}