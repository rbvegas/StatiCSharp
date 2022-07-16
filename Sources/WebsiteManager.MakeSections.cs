using StatiCSharp.Interfaces;

namespace StatiCSharp
{
    public partial class WebsiteManager : IWebsiteManager
    {
        /// <summary>
        /// Creates and writes the sections (not pages or items) of the website.
        /// </summary>
        private void MakeSections()
        {
            foreach (ISection site in Website.Sections)
            {
                string body = HtmlFactory.MakeSectionHtml(site);
                string head = HtmlFactory.MakeHeadHtml();
                string page = AddLeadingHtmlCode(Website, site, head, body);
                string path = Directory.CreateDirectory(Path.Combine(Output, site.SectionName)).ToString();

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
