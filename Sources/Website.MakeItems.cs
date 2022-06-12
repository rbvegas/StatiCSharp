using StatiCsharp.Interfaces;
using StatiCsharp.Tools;

namespace StatiCsharp
{
    public partial class Website: IWebsite
    {
        /// <summary>
        /// Creates and writes the items (not sections or pages) of the website with the given HtmlFactory.
        /// </summary>
        /// <param name="htmlFactory"></param>
        public void MakeItems(IHtmlFactory htmlFactory)
        {
            foreach (ISection currentSection in this.Sections)
            {
                foreach (IItem site in currentSection.Items)
                {
                    string body = htmlFactory.MakeItemHtml(site);
                    string head = htmlFactory.MakeHeadHtml();
                    string page = AddLeadingHtmlCode(this, site, head, body);
                    string defaultPath = FilenameToPath.From(site.MarkdownFileName);

                    string itemPath = (site.Path != string.Empty) ? site.Path : defaultPath;
                    string path = Directory.CreateDirectory(Path.Combine(_output, currentSection.SectionName, itemPath)).ToString();

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
}
