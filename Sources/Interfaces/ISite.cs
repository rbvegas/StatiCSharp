using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatiCsharp.Interfaces
{
    public interface ISite
    {
        string Title { get; set; }
        string Description { get; set; }
        string Author { get; set; }

        /// <summary>
        /// The date when this site is published or should be.
        /// </summary>
        DateOnly Date { get; set; }

        /// <summary>
        /// The date when the markdown-file was last modified.
        /// </summary>
        DateOnly DateLastModified { get; set; }
        
        // The path of the site within its corresponding hierarchy
        string Path { get; set; }
        List<string> Tags { get; set; }
        string Content { get; set; }

        /// <summary>
        ///  Filename of the file from where the site is generated
        /// </summary>
        string MarkdownFileName { get; set; }
    }
}
