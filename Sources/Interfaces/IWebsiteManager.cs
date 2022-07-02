using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatiCsharp.Interfaces
{
    internal interface IWebsiteManager
    {
        /// <summary>
        /// The absolute path to the directory that contains the directories `Content`, `Output` and `Resources`.
        /// </summary>
        string SourceDir { get; set; }

        /// <summary>
        /// The abolute path to the content (markdown-files) for the website.
        /// </summary>
        string Content { get; set; }

        /// <summary>
        /// The path to the resources (static files) of the website.
        /// </summary>
        string Resources { get; set; }

        /// <summary>
        /// The absolute path to the output directory.
        /// </summary>
        string Output { get; set; }

        /// <summary>
        /// If true, the site generator only writes files if there are any changes.<br/>
        /// If false, all output files are rewritten.
        /// </summary>
        bool GitMode { get; set; }

        /// <summary>
        /// The website of the current context.
        /// </summary>
        IWebsite Website { get; set; }

        /// <summary>
        /// The theme to build the website.
        /// </summary>
        IHtmlFactory HtmlFactory { get; set; }

        /// <summary>
        /// Starts the generating process of the website with the given theme.<br/>
        /// Files are saved in the `Output` directory.<br/><br/>
        /// If the `Output` directory is not explicity set, it is inside the `Source` directory.
        /// </summary>
        void Make();
    }
}
