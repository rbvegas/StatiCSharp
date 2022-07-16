using StatiCSharp.Interfaces;
using System.Text;

namespace StatiCSharp.HtmlComponents
{
    /// <summary>
    /// A representation of a <nav></nav> element.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    public class Nav : HtmlElement, IHtmlComponent
    {
        private protected override string TagName
        {
            get { return "nav"; }
        }

        public Nav()
        {
            // No action needed, because the base class already initialized an empty List<IHtmlComponent>.
        }

        /// <summary>
        /// Initiate a new nav element with another element or component inside.
        /// </summary>
        /// <param name="component">The element or component for the content of the nav tag.</param>
        public Nav(IHtmlComponent component)
        {
            Content = new List<IHtmlComponent> { component };
        }

        /// <summary>
        /// Initiate a new nav element with text.
        /// </summary>
        /// <param name="text">The text for the content of the nav tag.</param>
        public Nav(string text)
        {
            Content = new List<IHtmlComponent> { new Text(text) };
        }

    }
}
