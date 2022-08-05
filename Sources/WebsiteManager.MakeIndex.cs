using StatiCSharp.Interfaces;

namespace StatiCSharp;

public partial class WebsiteManager : IWebsiteManager
{
    /// <summary>
    /// Creates and writes the index (homepage) of the website.
    /// </summary>
    private void MakeIndex()
    {
        string body = HtmlFactory.MakeIndexHtml(Website.Index);
        string head = HtmlFactory.MakeHeadHtml();
        string index = AddLeadingHtmlCode(Website, Website.Index, head, body);
        WriteFile(Output, "index.html", index, gitMode: GitMode);
        PathDirectory.Add(Output);
    }
}
