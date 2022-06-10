using StatiCsharp.Interfaces;

namespace StatiCsharp
{
    /// <summary>
    /// Represenation of the index page.
    /// </summary>
    internal class Index : ISite
    {
        private string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string description = string.Empty;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string author = string.Empty;
        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        private DateOnly date = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly Date
        {
            get { return date; }
            set { date = value; }
        }

        private DateOnly dateLastModified = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly DateLastModified
        {
            get { return this.dateLastModified; }
            set { this.dateLastModified = value; }
        }

        private string path = string.Empty;
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public string Url
        {
            get { return "/"; }
        }

        private string markdownFileName = string.Empty;
        public string MarkdownFileName
        {
            get { return this.markdownFileName; }
            set { this.markdownFileName = value; }
        }

        private string markdownFilePath = string.Empty;
        public string MarkdownFilePath
        {
            get { return markdownFilePath; }
            set { this.markdownFilePath = value; }
        }

        private List<string> tags = new List<string>();
        public List<string> Tags
        {
            get { return tags; }
            set { tags = value; }
        }

        private string content = string.Empty;
        public string Content
        {
            get { return content; }
            set { content = value; }
        }
    }
}
