using System.Globalization; // CultureInfo
using System.IO; // GetCurrentDirectory, GetDirectories, CreateDirectory
using static System.Console;
using Markdig; // Markdown parser
using System.Reflection;
using StatiCsharp.Interfaces;
using System.Text; // StringBuilder

namespace StatiCsharp
{
    public partial class Website : IWebsite
    {
        private string url;
        public string Url
        {
            get { return url; }
        }

        private string name;
        public string Name
        {
            get { return name; }
        }

        private string description;
        public string Description
        {
            get { return description; }
        }

        private CultureInfo language;
        public CultureInfo Language
        {
            get { return language; }
        }

        private string output;
        public string? Output
        {
            get { return output; }
        }

        private ISite? index = new Index();
        public ISite? Index { get { return this.index; } }

        private List<ISite> pages = new List<ISite>();
        public List<ISite> Pages
        {
            get { return this.pages; }
            set { this.pages = value; }
        }

        private List<ISection> sections = new List<ISection>();
        public List<ISection> Sections
        {
            get { return this.sections; }
            set { this.sections = value; }
        }

        private List<string> makeSectionsFor = new List<string>();
        public List<string> MakeSectionsFor
        {
            get { return this.makeSectionsFor; }
            set { this.makeSectionsFor = value; }
        }

        private string content;
        public string Content { get { return this.content; } }

        private string resources = string.Empty;
        public string Resources { 
            get { return this.resources;} 
            set { this.resources = value; }
        }

        private string sourceDir;
        public string SourceDir
        {
            get { return this.sourceDir;}
            set { this.sourceDir = value; }
        }

        // Init
        public Website(string url, string name, string description, string language, string sections, string source)
        {
            this.url = url;
            this.name = name;
            this.description = description;
            this.language = new CultureInfo(language);
            this.sourceDir = Path.Combine(source);
            this.content = Path.Combine(source, "content");
            this.resources = Path.Combine(source, "resources");
            this.output = Path.Combine(source, "output");
            this.makeSectionsFor = sections.Replace(" ", string.Empty).Split(',').ToList();
        }

        public void Make()
        {
            IHtmlFactory factory = new DefaultHtmlFactory();
            factory.WithWebsite(this);
            this.Make(factory);
        }

        public void Make(IHtmlFactory HtmlFactory)
        {
            HtmlFactory.WithWebsite(this);
            WriteLine("Making your website...");

            WriteLine("Collecting markdown data...");
            GenerateSitesFromMarkdown(this);

            WriteLine("Deleting old output files...");
            DeleteAll(this.output);

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

            WriteLine("Copying resources");
            CopyAll(this.Resources, output);
            File.Copy(HtmlFactory.ResourcePaths, Path.Combine(output, "styles.css"), true);
        }
    }
}