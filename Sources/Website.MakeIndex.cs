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
        /// Creates and writes the index/homepage of the website with the given HtmlFactory.
        /// </summary>
        /// <param name="HtmlFactory"></param>
        private void MakeIndex(IHtmlFactory HtmlFactory)
        {
            string body = HtmlFactory.MakeIndexHtml(this);
            string index = AddLeadingHtmlCode(this, this.Index, body);
            WriteFile(output, "index.html", index, gitMode: this.gitMode);
            this.PathDirectory.Add(output);
        }
    }
}
