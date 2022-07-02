using StatiCsharp.Interfaces;
using System.Text;

namespace StatiCsharp
{
    public partial class WebsiteManager : IWebsiteManager
    {
        /// <summary>
        /// Add the leading (and trailing) html code to a site.
        /// </summary>
        /// <param name="website">The website, containing information for the head.</param>
        /// <param name="context">The site where the html-code should be added.</param>
        /// <param name="body">The body for the the, rendered by a HtmlFactory.</param>
        /// <param name="head">The additional content for the head of the site.</param>
        /// <returns>The content for a html-file as a string.</returns>
        private string AddLeadingHtmlCode(IWebsite website, ISite context, string head, string body)
        {
            StringBuilder siteBuilder = new StringBuilder();
            siteBuilder.Append("<!doctype html>");
            siteBuilder.Append($"<html lang=\"{website.Language.Name}\">");
            siteBuilder.Append("<head>");
            siteBuilder.Append("<meta charset=\"utf-8\">");
            siteBuilder.Append("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
            siteBuilder.Append($"<title>{context.Title}</title>");
            siteBuilder.Append($"<meta name=\"description\" content=\"{website.Description}\">");
            siteBuilder.Append("<link rel=\"icon\" type=\"image/x-icon\" href=\"/favicon.png\">");
            siteBuilder.Append(head);
            siteBuilder.Append("</head>");
            siteBuilder.Append(body);
            siteBuilder.Append("</html>");

            return siteBuilder.ToString();
        }
    }
}
