using StatiCsharp.Interfaces;
using System.Text;

namespace StatiCsharp.HtmlComponents
{
    /// <summary>
    /// A representation of a <article></article> element.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    public class Article : HtmlElement, IHtmlComponent
    {
        private protected override string TagName
        {
            get { return "article"; }
        }

        /// <summary>
        /// Initiate a new empty <article> element.
        /// </summary>
        public Article()
        {
            // No action needed, because the base class already initialized an empty List<IHtmlComponent>.
        }

        /// <summary>
        /// Initiate a new <article> with another element or component inside.
        /// </summary>
        /// <param name="component">The element or component for the content of the <article>.</param>
        public Article(IHtmlComponent component)
        {
            Content = new List<IHtmlComponent>() { component };
        }

        /// <summary>
        /// Initiate a new <article> element with text.
        /// </summary>
        /// <param name="text">The text for the content of the <article>.</param>
        public Article(string text)
        {
            Content = new List<IHtmlComponent>() { new Text(text) };
        }

    }
}
