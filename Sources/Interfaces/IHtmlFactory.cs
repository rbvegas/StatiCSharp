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
        /// Method that returns the html-code for the index.
        /// </summary>
        /// /// <param name="website">The websites index to render.</param>
        /// <returns>A string containing the html-code.</returns>
        public string MakeIndexHtml(IWebsite website);

        /// <summary>
        /// Method that returns the html-code for a page (not section or item).
        /// </summary>
        /// <param name="page">The page to render.</param>
        /// <returns></returns>
        public string MakePageHtml(IPage page);
    }
}
