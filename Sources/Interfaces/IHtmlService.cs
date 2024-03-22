namespace StatiCSharp.Interfaces;

internal interface IHtmlService
{
    /// <summary>
    /// Add the leading (and trailing) html code to a site and returns the generated HTML code as a string.
    /// </summary>
    /// <param name="website">The website, containing information for the head.</param>
    /// <param name="context">The site where the html-code should be added.</param>
    /// <param name="body">The body for the the, rendered by a HtmlFactory.</param>
    /// <param name="head">The additional content for the head of the site.</param>
    /// <returns>The content for a html-file as a string.</returns>
    string AddLeadingHtmlCode(IWebsite website, ISite context, string head, string body, HtmlBuilder htmlBuilder);
}