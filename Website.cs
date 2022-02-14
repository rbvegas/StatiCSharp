using System.Globalization; // CultureInfo
using System.IO; // GetCurrentDirectory
using static System.Console;
using Markdig; // Markdown parser
using System.Reflection;

namespace StatiCsharp
{

    public interface IWebsite
    {
        /// The absolute url that the website will be hosted at.
        string Url { get; }

        /// The name of the website.
        string Name { get; }

        /// A description of the website.
        string Description { get; }

        /// The website's primary language
        CultureInfo Language { get; }

        /// The website's favicon, if any.
        //TODO

    }
    public class Website : IWebsite
    {
        private string url;
        public string Url
        {
            get {return url; }
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

        internal string content;

        internal Index? Index;

        // Init
        public Website()
        {
            url         = "URL/of/my/awesome/website";
            name        = "name of my site";
            description = "descripton of site";
            language    = new CultureInfo("en-US");
            content     = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "StatiCsharpEnv", "Content");
            output      = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "StatiCsharpEnv", "Output");
        }

        public Website(string url, string name, string description, string language)
        {
            this.url            = url;
            this.name           = name;
            this.description    = description;
            this.language       = new CultureInfo(language);
            content = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "StatiCsharpEnv", "Content");
            output = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "StatiCsharpEnv", "Output");
        }

        public void Make()
        {
            WriteLine("Making your website...");

            WriteLine("Generating index page...");
            MakeIndex();
        }

        private void MakeIndex()
        {
            WriteLine("Making your index page...");
            string pathToMd = Path.Combine(this.content, "index.md");

            // Read meta data of md-file
            Dictionary<string, string> metaData = MarkdownFactory.ParseMetaData(pathToMd);

            // Read content of md file
            string content = Markdown.ToHtml(MarkdownFactory.ParseContent(pathToMd));

            string index = HtmlFactory.MakeIndexHtml();

            File.WriteAllText(Path.Combine(output, "index.html"), index);
        }
    }
}