namespace StatiCsharp.Interfaces
{
    /// <summary>
    /// Interface to implement for making StatiC# compatible custom themes.
    /// </summary>
    public interface IHtmlFactory
    {
        /// <summary>
        /// Path to the corresponding resources. All files within this directory will be copied to the root directory of the output.
        /// </summary>
        public string ResourcesPath { get; }

        /// <summary>
        /// The website the theme is used for. So that the theme can access additional information.
        /// </summary>
        public IWebsite? Website { get; set; }

        /// <summary>
        /// Method to inject a website into the theme.
        /// </summary>
        /// <param name="website"></param>
        public void WithWebsite(IWebsite website)
        {
            this.Website = website;
        }

        /// <summary>
        /// Creates html-code for inside the &lt;head&gt;&lt;/head&gt;-tag. This code is added to all sites.
        /// </summary>
        /// <returns>A string containing the html-code.</returns>
        public string MakeHeadHtml();

        /// <summary>
        /// Method that returns the html-code for the index site.
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

        /// <summary>
        /// Method that returns the html-code for the taglist site.
        /// </summary>
        /// <param name="website"></param>
        /// <returns>A string containing the html-code.</returns>
        public string MakeTagListHtml(List<IItem> items, string tag);
    }
}
