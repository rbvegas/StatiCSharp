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

        /// <summary>
        /// The path of the site within its hierachy, given by the users meta data.
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// The relative url of the item.
        /// </summary>
        public string Url { get; }

        List<string> Tags { get; set; }
        string Content { get; set; }

        /// <summary>
        ///  Filename of the file from where the site is generated
        /// </summary>
        string MarkdownFileName { get; set; }

        /// <summary>
        /// Path of the markdown-file from where the site in generated
        /// </summary>
        string MarkdownFilePath { get; set; }
    }
}
