using StatiCSharp.Interfaces;
using StatiCSharp.Tools;

namespace StatiCSharp
{
    public partial class WebsiteManager : IWebsiteManager
    {
        /// <summary>
        /// Creates and writes the items (not sections or pages) of the website.
        /// </summary>
        private void MakeItems()
        {
            foreach (ISection currentSection in Website.Sections)
            {
                foreach (IItem site in currentSection.Items)
                {
                    string body = HtmlFactory.MakeItemHtml(site);
                    string head = HtmlFactory.MakeHeadHtml();
                    string page = AddLeadingHtmlCode(Website, site, head, body);
                    string defaultPath = FilenameToPath.From(site.MarkdownFileName);

                    string itemPath = (site.Path != string.Empty) ? site.Path : defaultPath;
                    string path = Directory.CreateDirectory(Path.Combine(Output, currentSection.SectionName, itemPath)).ToString();

                    if (this.PathDirectory.Contains(path))
                    {
                        Console.WriteLine($"WARNING: The path {path} is allready in use. Change the path in meta data to avoid duplicates.");
                    }

                    WriteFile(path: path, filename: "index.html", content: page, gitMode: GitMode);

                    PathDirectory.Add(path);
                }
            }
        }
    }
}
