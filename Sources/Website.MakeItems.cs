using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatiCsharp.Interfaces;
using StatiCsharp.Tools;

namespace StatiCsharp
{
    public partial class Website: IWebsite
    {
        /// <summary>
        /// Creates and writes the items (not sections or pages) of the website with the given HtmlFactory.
        /// </summary>
        /// <param name="HtmlFactory"></param>
        public void MakeItems(IHtmlFactory HtmlFactory)
        {
            foreach (ISection currentSection in this.Sections)
            {
                foreach (IItem site in currentSection.Items)
                {
                    string body = HtmlFactory.MakeItemHtml(site);
                    string page = AddLeadingHtmlCode(this, site, body);
                    string defaultPath = FilenameToPath.From(site.MarkdownFileName);

                    string itemPath = (site.Path != string.Empty) ? site.Path : defaultPath;
                    string path = Directory.CreateDirectory(Path.Combine(output, currentSection.SectionName, itemPath)).ToString();
                    
                    WriteFile(path: path, filename: "index.html", content: page, gitMode: this.gitMode);

                    this.PathDirectory.Add(path);
                }
            }
        }
    }
}
