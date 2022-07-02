﻿using StatiCsharp.Interfaces;

namespace StatiCsharp
{
    public partial class WebsiteManager : IWebsiteManager
    {
        /// <summary>
        /// Creates and writes the index/homepage of the website.
        /// </summary>
        /// <param name="htmlFactory"></param>
        private void MakeIndex()
        {
            string body = HtmlFactory.MakeIndexHtml(Website);
            string head = HtmlFactory.MakeHeadHtml();
            string index = AddLeadingHtmlCode(Website, Website.Index, head, body);
            WriteFile(Output, "index.html", index, gitMode: GitMode);
            PathDirectory.Add(Output);
        }
    }
}
