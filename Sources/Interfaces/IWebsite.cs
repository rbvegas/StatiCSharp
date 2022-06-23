using System.Globalization; // CultureInfo

namespace StatiCsharp.Interfaces
{
    /// <summary>
    /// Interface to confrom to for a website object while building templates.
    /// </summary>
    public interface IWebsite
    {
        /// <summary>
        /// The absolute domain of the website. E.g. "https://mydomain.com".
        /// </summary>
        string Url { get; }

        /// <summary>
        /// The name of the website.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// A short description of the website. Is used for metadata in the html sites.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The language the websites main content is written in.
        /// </summary>
        CultureInfo Language { get; }

        /// <summary>
        /// The abolute path to the content (markdown-files) for the website.
        /// </summary>
        string Content { get; }

        /// <summary>
        ///  The path to the resources (static files) of the website.
        /// </summary>
        string Resources { get; }

        /// <summary>
        /// The absolute path to the output directory.
        /// </summary>
        string Output { get; }

        /// <summary>
        /// The absolute path to the directory that contains the directories `Content`, `Output` and `Resources`.
        /// </summary>
        string SourceDir { get; }

        /// The website's favicon, if any.
        //TODO

        /// <summary>
        /// Represents the index (homepage) of the website.
        /// </summary>
        ISite Index { get; }

        /// <summary>
        /// The collection of pages the website contains.
        /// </summary>
        List<IPage> Pages { get; }

        /// <summary>
        /// The collection of sections the website's sections (not pages).
        /// </summary>
        List<ISection> Sections { get; }

        /// <summary>
        /// Collection of the websites section-names. Folders in the content directory with names matching one item of this list a treated as sections.
        /// </summary>
        List<string> MakeSectionsFor { get; }

    }
}
