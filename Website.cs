using System.Globalization; // CultureInfo
using System.IO; // GetCurrentDirectory, GetDirectories, CreateDirectory
using static System.Console;
using Markdig; // Markdown parser
using System.Reflection;
using StatiCsharp.Interfaces;
using System.Text; // StringBuilder

namespace StatiCsharp
{
    public class Website : IWebsite
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

        private List<ISite> sections = new List<ISite>();
        public List<ISite> Sections
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

        

        // Init
        public Website()
        {
            url = "URL/of/my/awesome/website";
            name = "name of my site";
            description = "descripton of site";
            language = new CultureInfo("en-US");
            content = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "StatiCsharpEnv", "Content");
            output = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "StatiCsharpEnv", "Output");
            makeSectionsFor = new List<string>() {"physics", "dev"};
        }

        public Website(string url, string name, string description, string language)
        {
            this.url = url;
            this.name = name;
            this.description = description;
            this.language = new CultureInfo(language);
            content = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "StatiCsharpEnv", "Content");
            output = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "StatiCsharpEnv", "Output");
        }

        public void Make()
        {
            this.Make(new StatiCsharp.FoundationHtmlFactory());
        }

        public void Make(IHtmlFactory HtmlFactory)
        {
            WriteLine("Making your website...");

            WriteLine("Collecting markdown data...");
            GenerateSitesFromMarkdown(this);

            WriteLine("Generating index page...");
            MakeIndex(HtmlFactory);

            WriteLine("Generating pages...");
            MakePages(HtmlFactory);

        }

        private void GenerateSitesFromMarkdown(IWebsite website)
        {
            string content = website.Content;

            // For the index: Collecting meta data and content from markdown files.
            Dictionary<string, string> mdMetaData    = MarkdownFactory.ParseMetaData(Path.Combine(content, "index.md"));
            string mdContent                         = MarkdownFactory.ParseContent(Path.Combine(content, "index.md"));
            website.Index.Content                    = Markdown.ToHtml(MarkdownFactory.ParseContent(Path.Combine(content, "index.md")));
            MapMetaData(mdMetaData, website.Index);

            // Collecting pages and sections data
            string[] directoriesOfContent = Directory.GetDirectories(content);
            foreach (string directory in directoriesOfContent)
            {
                string nameOfCurrentDirectory = Path.GetFileName(directory);
                if (!website.MakeSectionsFor.Contains(nameOfCurrentDirectory))
                {
                    string[] filenames = Directory.GetFiles(directory);
                    for (int i = 0; i < filenames.Length; i++)
                    {
                        filenames[i] = Path.GetFileName(filenames[i]);
                    }

                    if (filenames.Length > 0)
                    {
                        foreach (string filename in filenames)
                        {
                            IPage currentPage = new Page();
                            Dictionary<string, string> currentMetaData = MarkdownFactory.ParseMetaData(Path.Combine(content, directory, filename));
                            currentPage.Content = Markdown.ToHtml(MarkdownFactory.ParseContent(Path.Combine(content, directory, filename)));
                            MapMetaData(currentMetaData, currentPage);
                            currentPage.Hierarchy = nameOfCurrentDirectory;
                            website.Pages.Add(currentPage);
                        }
                    }
                }
                else
                {
                    // Add a section page
                }
            }
        }

        /// <summary>
        /// Add the leading (and trailing) html code to a site.
        /// </summary>
        /// <param name="website">The website, containing information for the head.</param>
        /// <param name="context">The site where the html-code should be added.</param>
        /// <param name="body">The body for the the, rendered by a HtmlFactory</param>
        /// <returns>The content for a html-file as a string.</returns>
        private string AddLeadingHtmlCode(IWebsite website, ISite context, string body)
        {
            StringBuilder siteBuilder = new StringBuilder();
            siteBuilder.Append("<!doctype html>");
            siteBuilder.Append($"<html lang=\"{website.Language.Name}\">");
            siteBuilder.Append("<head>");
            siteBuilder.Append("<meta charset=\"utf-8\">");
            siteBuilder.Append("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
            siteBuilder.Append($"<title>{context.Title}</title>");
            siteBuilder.Append($"<meta name=\"description\" content=\"{website.Description}\">");
            siteBuilder.Append("</head>");
            siteBuilder.Append("<body>");
            siteBuilder.Append(body);
            siteBuilder.Append("</body>");
            siteBuilder.Append("</html>");
            
            return siteBuilder.ToString();
        }

        /// <summary>
        /// Adds the given meta data to a site (index, page, section or item). If there is no field for a given entry, it's sciped.
        /// </summary>
        /// <param name="metaData">The meta data.</param>
        /// <param name="site">The site where to add the meta data.</param>
        private void MapMetaData(Dictionary<string,string> metaData, ISite site)
        {
            try { if (metaData["title"] is not null) { site.Title = metaData["title"]; } } catch { }
            try { if (metaData["description"] is not null) { site.Description = metaData["description"]; } } catch { }
            try { if (metaData["author"] is not null) { site.Author = metaData["author"]; } } catch { }
            try { if (metaData["date"] is not null) { site.Date = DateOnly.Parse(metaData["date"]); } } catch { }
            try { if (metaData["path"] is not null) { site.Path = metaData["path"]; } } catch { }
            try { if (metaData["tags"] is not null) { site.Tags = metaData["tags"].Replace(" ", string.Empty).Split(',').ToList(); } } catch { }
        }

        /// <summary>
        /// Creates and writes the index/homepage of the website with the given HtmlFactory.
        /// </summary>
        /// <param name="HtmlFactory"></param>
        private void MakeIndex(IHtmlFactory HtmlFactory)
        {
            string body = HtmlFactory.MakeIndexHtml(this);
            string index = AddLeadingHtmlCode(this, this.Index, body);
            File.WriteAllText(Path.Combine(output, "index.html"), index);
        }

        private void MakePages(IHtmlFactory HtmlFactory)
        {
            foreach (IPage site in this.Pages)
            {
                /////// HERER WE ARE. 
                string body = HtmlFactory.MakePageHtml(site);
                string page = AddLeadingHtmlCode(this, site, body);
                // Create directory, if it does not excist.
                string path = Directory.CreateDirectory(Path.Combine(output, site.Hierarchy)).ToString();
                File.WriteAllText(Path.Combine(path, "index.html"), page);
            }
        }
    }
}