using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatiCsharp
{   
    /// <summary>
    /// Interface that all html-components must implement to ensure the output/render as a string containing html-code.
    /// </summary>
    internal interface IHtmlComponent
    {
        /// <summary>
        /// Renders the html component as a string.
        /// </summary>
        /// <returns>A string containing the components html-code.</returns>
        internal string Render();
    }
}
