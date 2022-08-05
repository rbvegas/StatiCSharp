using StatiCSharp.Interfaces;
using StatiCSharp.Tools;

namespace StatiCSharp;

public partial class WebsiteManager : IWebsiteManager
{
    /// <summary>
    /// Creates and writes the pages (not sections or items) of the website.
    /// </summary>
    private void MakePages()
    {
        foreach (IPage site in Website.Pages)
        {
            string body = HtmlFactory.MakePageHtml(site);
            string head = HtmlFactory.MakeHeadHtml();
            string page = AddLeadingHtmlCode(Website, site, head, body);
            string defaultPath = FilenameToPath.From(site.MarkdownFileName);

            // Create directory, if it does not excist.
            string pathInHierachy = (site.Path == string.Empty) ? defaultPath : site.Path;
            if (pathInHierachy == "index") { pathInHierachy = string.Empty; }
            string path = Directory.CreateDirectory(Path.Combine(Output, site.Hierarchy, pathInHierachy)).ToString();

            if (this.PathDirectory.Contains(path))
            {
                Console.WriteLine($"WARNING: The path {path} is allready in use. Change the path in meta data to avoid duplicates.");
            }

            WriteFile(path: path, filename: "index.html", content: page, gitMode: GitMode);

            PathDirectory.Add(path);
        }
    }
}
