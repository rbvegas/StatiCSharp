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
            string index = AddLeadingHtmlCode(this, this.Index, head, body);
            WriteFile(_output, "index.html", index, gitMode: this._gitMode);
            this.PathDirectory.Add(_output);
        }
    }
}
