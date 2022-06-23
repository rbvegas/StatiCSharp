using StatiCsharp.Interfaces;
using System.Text;

namespace StatiCsharp.HtmlComponents
{
    /// <summary>
    /// A representation of an <a href></a> element.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    public class A : HtmlElement, IHtmlComponent
    {
        private protected override string TagName
        {
            get { return "a"; }
        }

        /// <summary>
        /// Initiate a new and empty anker-element.
        /// </summary>
        public A()
        {
            // No action needed, because the base class already initialized an empty List<IHtmlComponent>.
        }

        /// <summary>
        /// Initiate a new link with another element or component inside.
        /// </summary>
        /// <param name="component"></param>
        public A(IHtmlComponent component)
        {
            Content = new List<IHtmlComponent>() { component };
        }

        /// <summary>
        /// Initiate a new link with a text-body.
        /// </summary>
        /// <param name="text">The text for the link.</param>
        public A(string text)
        {
            Content = new List<IHtmlComponent>();
            Content.Add(new Text(text));
        }

        /// <summary>
        /// Set the content of the href attribute.
        /// </summary>
        /// <param name="href">The target of the link.</param>
        /// <returns>this - the element itself.</returns>
        public A Href(string href)
        {
            if (!Attributes.TryAdd("href", href))
            {
                Attributes["href"] = href;
            }
            return this;
        }

    }
}
