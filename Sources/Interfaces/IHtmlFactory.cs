using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatiCsharp.Interfaces
{
    public interface IHtmlFactory
    {
        /// <summary>
        /// Path to the corresponding css file.
        /// </summary>
        public string cssPath { get; }

        /// <summary>
        /// The website the theme is used for.
        /// </summary>
        public IWebsite? Website { get; }

        /// <summary>
        /// Method to add a website to the theme.
        /// </summary>
        /// <param name="website"></param>
        public void WithWebsite(IWebsite website);

        /// <summary>
        /// Method that returns the html-code for the index.
        /// </summary>
        /// /// <param name="website">The websites index to render.</param>
        /// <returns>A string containing the html-code.</returns>
        public string MakeIndexHtml(IWebsite website);

        /// <summary>
        /// Method that returns the html-code for a page (not section or item).
        /// </summary>
        /// <param name="page">The page to render.</param>
        /// <returns>A string containing the html-code.</returns>
        public string MakePageHtml(IPage page);

        /// <summary>
        /// Method that returns the html-code for a section site.
        /// </summary>
        /// <param name="section">The section to render.</param>
        /// <returns>A string containing the html-code.</returns>
        public string MakeSectionHtml(ISection section);

        /// <summary>
        /// Method that returns the html-code for an item site.
        /// </summary>
        /// <param name="item">The item to render.</param>
        /// <returns>A string containing the html-code.</returns>
        public string MakeItemHtml(IItem item);
    }
}
