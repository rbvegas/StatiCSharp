using StatiCsharp.Interfaces;

namespace StatiCsharp
{
    public partial class Website: IWebsite
    {
        /// <summary>
        /// Creates and writes the index/homepage of the website with the given HtmlFactory.
        /// </summary>
        /// <param name="htmlFactory"></param>
        private void MakeIndex(IHtmlFactory htmlFactory)
        {
            string body = htmlFactory.MakeIndexHtml(this);
            string head = htmlFactory.MakeHeadHtml();
            string index = AddLeadingHtmlCode(this, Index, head, body);
            WriteFile(Output, "index.html", index, gitMode: GitMode);
            PathDirectory.Add(Output);
        }
    }
}
