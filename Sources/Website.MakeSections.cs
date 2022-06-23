using StatiCsharp.Interfaces;

namespace StatiCsharp
{
    public partial class Website: IWebsite
    {
        /// <summary>
        /// Creates and writes the sections (not pages or items) of the website with the given HtmlFactory.
        /// </summary>
        /// <param name="htmlFactory"></param>
        private void MakeSections(IHtmlFactory htmlFactory)
        {
            foreach (ISection site in this.Sections)
            {
                string body = htmlFactory.MakeSectionHtml(site);
                string head = htmlFactory.MakeHeadHtml();
                string page = AddLeadingHtmlCode(this, site, head, body);
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
