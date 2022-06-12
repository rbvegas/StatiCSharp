using StatiCsharp.Interfaces;

namespace StatiCsharp
{
    public partial class Website: IWebsite
    {
        /// <summary>
        /// Creates and writes the index/homepage of the website with the given HtmlFactory.
        /// </summary>
        /// <param name="HtmlFactory"></param>
        private void MakeIndex(IHtmlFactory HtmlFactory)
        {
            string body = HtmlFactory.MakeIndexHtml(this);
            string head = HtmlFactory.MakeHeadHtml();
            string index = AddLeadingHtmlCode(this, this.Index, head, body);
            WriteFile(_output, "index.html", index, gitMode: this._gitMode);
            this.PathDirectory.Add(_output);
        }
    }
}
