using StatiCsharp.Interfaces;
using System.Text;

namespace StatiCsharp.HtmlComponents
{
    /// <summary>
    /// A representation of a <span></span> element.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    public class Span : HtmlElement, IHtmlComponent
    {
        private protected override string TagName
        {
            get { return "span"; }
        }

        /// <summary>
        /// Initiate a new empty <span> element.
        /// </summary>
        public Span()
        {
            // No action needed, because the base class already initialized an empty List<IHtmlComponent>.
        }

        /// <summary>
        /// Initiate a new span element with another element or component inside.
        /// </summary>
        /// <param name="component">The element or component for the content of the span.</param>
        public Span(IHtmlComponent component)
        {
            Content = new List<IHtmlComponent>() { component };
        }

        /// <summary>
        /// Initiate a new span element with text.
        /// </summary>
        /// <param name="text">The text for the content of the span.</param>
        public Span(string text)
        {
            Content = new List<IHtmlComponent>() { new Text(text) };
        }

    }
}
