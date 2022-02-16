using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatiCsharp.Interfaces;

namespace StatiCsharp
{
    /// <summary>
    /// Represenation of a page.
    /// </summary>
    internal class Page : IPage
    {
        private string title =string.Empty;
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        private string description = string.Empty;
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        private string author = "";
        public string Author 
        {
            get { return this.author; }
            set { this.author = value; }
        }

        private DateOnly date = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly Date
        {
            get { return this.date; }
            set { this.date = value; }
        }

        private string path = string.Empty;
        public string Path
        {
            get { return this.path;}
            set { this.path = value; }
        }

        private string hierarchy = string.Empty;
        public string Hierarchy
        {
            get { return this.hierarchy; }
            set { this.hierarchy = value; }
        }

        private List<string> tags = new List<string>();
        public List<string> Tags
        {
            get { return this.tags;}
            set { this.tags = value; }
        }

        private string content = string.Empty;
        public string Content
        {
            get { return this.content; }
            set { this.content = value; }
        }
    }
}
