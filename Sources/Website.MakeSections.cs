using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                string path = Directory.CreateDirectory(Path.Combine(output, site.SectionName)).ToString();
                
                WriteFile(path: path, filename: "index.html", content: page, gitMode: this.gitMode);
            }
        }
    }
}
