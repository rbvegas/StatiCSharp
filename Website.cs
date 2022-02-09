using System.Globalization; // CultureInfo

namespace StatiCsharp
{

    public interface IWebsite
    {
        /// The absolute url that the website will be hosted at.
        string Url { get; }

        /// The name of the website.
        string Name { get; }

        /// A description of the website.
        string Description { get; }

        /// The website's primary language
        CultureInfo language { get; }

        /// The website's favicon, if any.
        //TODO
        
    }
    public class Website
    {

    }
}