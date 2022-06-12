using StatiCsharp.Interfaces;
using StatiCsharp.Tools;

namespace StatiCsharp
{
    public partial class Website: IWebsite
    {
        /// <summary>
        /// Creates and writes the pages (not sections or items) of the website with the given HtmlFactory.
        /// </summary>
        /// <param name="htmlFactory"></param>
        private void MakePages(IHtmlFactory htmlFactory)
        {
            foreach (IPage site in this.Pages)
            {
                string body = htmlFactory.MakePageHtml(site);
                string head = htmlFactory.MakeHeadHtml();
                string page = AddLeadingHtmlCode(this, site, head, body);
                string defaultPath = FilenameToPath.From(site.MarkdownFileName);

                // Create directory, if it does not excist.
                string pathInHierachy = (site.Path == string.Empty) ? defaultPath : site.Path;
                if (pathInHierachy == "index") { pathInHierachy = string.Empty; }
                string path = Directory.CreateDirectory(Path.Combine(_output, site.Hierarchy, pathInHierachy)).ToString();

                if (this.PathDirectory.Contains(path))
                {
                    Console.WriteLine($"WARNING: The path {path} is allready in use. Change the path in meta data to avoid duplicates.");
                }

                WriteFile(path: path, filename: "index.html", content: page, gitMode: this._gitMode);

                this.PathDirectory.Add(path);
            }
        }
    }
}
