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
        /// <summary>
        /// The absolute url that the website will be hosted at.
        /// </summary>
        string Url { get; }

        /// <summary>
        /// The name of the website.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// A description of the website.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The website's primary language.
        /// </summary>
        CultureInfo Language { get; }

        /// <summary>
        /// The path to the content (markdown files) of the website.
        /// </summary>
        string Content { get; }

        /// <summary>
        ///  The path to the resources (static files) of the website.
        /// </summary>
        string Resources { get; }

        /// <summary>
        /// The source directory, containing Content, Ouput styles etc.
        /// </summary>
        string SourceDir { get; }

        /// The website's favicon, if any.
        //TODO

        /// <summary>
        /// The website's meta data and content for the index page.
        /// </summary>
        ISite Index { get; }

        /// <summary>
        /// The website's pages.
        /// </summary>
        List<ISite> Pages { get; }

        /// <summary>
        /// The website's sections (not pages).
        /// </summary>
        List<ISection> Sections { get; }

        /// <summary>
        /// The names of the sections the website will have. Need to be the exact name of the folders in the Content directory.
        /// Sections are also visible in the navigation.
        /// </summary>
        List<string> MakeSectionsFor { get; }
    }
}
