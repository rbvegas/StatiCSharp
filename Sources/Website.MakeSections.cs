using StatiCsharp.Interfaces;

namespace StatiCsharp
{
    public partial class Website: IWebsite
    {
        /// <summary>
        /// Creates and writes the sections (not pages or items) of the website with the given HtmlFactory.
        /// </summary>
        /// <param name="HtmlFactory"></param>
        private void MakeSections(IHtmlFactory HtmlFactory)
        {
            foreach (ISection site in this.Sections)
            {
                string body = HtmlFactory.MakeSectionHtml(site);
                string page = AddLeadingHtmlCode(this, site, body);
                string path = Directory.CreateDirectory(Path.Combine(_output, site.SectionName)).ToString();

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
