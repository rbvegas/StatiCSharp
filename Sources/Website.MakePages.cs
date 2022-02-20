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
        /// Creates and writes the pages (not sections or items) of the website with the given HtmlFactory.
        /// </summary>
        /// <param name="HtmlFactory"></param>
        private void MakePages(IHtmlFactory HtmlFactory)
        {
            foreach (IPage site in this.Pages)
            {
                string body = HtmlFactory.MakePageHtml(site);
                string page = AddLeadingHtmlCode(this, site, body);
                // Create directory, if it does not excist.
                string pathInHierachy = (site.Path == string.Empty) ? site.MarkdownFileName : site.Path;
                if (pathInHierachy == "index") { pathInHierachy = string.Empty; }
                string path = Directory.CreateDirectory(Path.Combine(output, site.Hierarchy, pathInHierachy)).ToString();
                File.WriteAllText(Path.Combine(path, "index.html"), page);
            }
        }
    }
}
