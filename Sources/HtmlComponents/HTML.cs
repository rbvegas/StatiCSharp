using StatiCsharp.Interfaces;
using System.Text;

namespace StatiCsharp.HtmlComponents
{
    /// <summary>
    /// A representation of a HTML element.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    public class HTML : HtmlElement, IHtmlComponent
    {
        private protected override string TagName
        {
            get { return "html"; }
        }

        /// <summary>
        /// Initiate a new empty html element.
        /// </summary>
        public HTML()
        {
            // No action needed, because the base class already initialized an empty List<IHtmlComponent>.
        }

        /// <summary>
        /// Initiate a new html element with another element or component inside.
        /// </summary>
        /// <param name="element">The element or component for the content of the html tag.</param>
        public HTML(IHtmlComponent element)
        {
            Content = new List<IHtmlComponent>();
            Content.Add(element);
        }

        /// <summary>
        /// Initiate a new html element with text.
        /// </summary>
        /// <param name="text">The text for the content of the html tag.</param>
        public HTML(string text)
        {
            Content = new List<IHtmlComponent>();
            Content.Add(new Text(text));
        }

    }
}
