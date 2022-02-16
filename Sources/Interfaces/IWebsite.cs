using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization; // CultureInfo

namespace StatiCsharp.Interfaces
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
        CultureInfo Language { get; }

        /// The path to the content (markdown files) of the website
        string Content { get; }

        /// The website's favicon, if any.
        //TODO

        /// The website's meta data and content for the index page
        ISite? Index { get; }

        /// The website's pages
        List<ISite> Pages { get; }

        /// The website's sections (not pages)
        List<ISite> Sections { get; }

        /// The names of the sections the website will have. Need to be the exact name of the folders in the Content directory.
        /// Sections are also visible in the navigation.
        List<string> MakeSectionsFor { get; }
    }
}
