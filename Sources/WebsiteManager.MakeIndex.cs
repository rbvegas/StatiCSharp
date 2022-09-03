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

    /// <summary>
    /// Asynchronously creates and writes the index (homepage) of the website.
    /// </summary>
    /// <returns>A task that represents the asynchronous index generating operation.</returns>
    private async Task MakeIndexAsync()
    {
        string body = HtmlFactory.MakeIndexHtml(Website.Index);
        string head = HtmlFactory.MakeHeadHtml();
        string index = AddLeadingHtmlCode(Website, Website.Index, head, body);
        await WriteFileAsync(Output, "index.html", index, gitMode: GitMode);
        PathDirectory.Add(Output);
    }
}
