using System.Text;
using StatiCSharp.Interfaces;

namespace StatiCSharp.Services;

internal class HtmlService : IHtmlService
{
    public string AddLeadingHtmlCode(IWebsite website, ISite context, string head, string body, HtmlBuilder htmlBuilder)
    {
        StringBuilder siteBuilder = new StringBuilder();
        siteBuilder.Append("<!doctype html>");
        siteBuilder.Append($"<html lang=\"{website.Language.Name}\">");
        siteBuilder.Append("<head>");
        siteBuilder.Append("<meta charset=\"utf-8\">");
        siteBuilder.Append("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
        siteBuilder.Append($"<title>{context.Title}</title>");
        siteBuilder.Append($"<meta name=\"description\" content=\"{context.Description}\">");
        siteBuilder.Append($"<meta name=\"author\" content=\"{context.Author}\">");
        siteBuilder.Append($"<meta name=\"keywords\" content=\"{string.Join(", ", context.Tags)}\">");
        siteBuilder.Append("<link rel=\"icon\" type=\"image/x-icon\" href=\"/favicon.png\">");
        siteBuilder.Append(head);
        siteBuilder.Append(htmlBuilder.AdditionalHeaderContent);
        siteBuilder.Append("</head>");
        siteBuilder.Append(body);
        siteBuilder.Append("</html>");

        return siteBuilder.ToString();
    }
}