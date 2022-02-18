using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatiCsharp.HtmlComponents
{
    /// <summary>
    /// A representation of an anker element.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    internal class A: IHtmlComponent
    {
        /// <summary>
        /// Contains the components inside the anker tag.
        /// </summary>
        private List<IHtmlComponent> content;

        /// <summary>
        /// The content of the href tag.
        /// </summary>
        private string? href;

        /// <summary>
        /// CSS classes
        /// </summary>
        private string? cssClass;

        /// <summary>
        /// Styles
        /// </summary>
        private string? cssStyle;

        /// <summary>
        /// Initiate a new and empty anker-element.
        /// </summary>
        public A()
        {
            this.content = new List<IHtmlComponent>();
        }

        /// <summary>
        /// Initiate a new link with a text-body.
        /// </summary>
        /// <param name="text">The text for the link.</param>
        public A(string text)
        {
            this.content = new List<IHtmlComponent>();
            this.content.Add(new Text(text));
        }

        /// <summary>
        /// Add a new element to the body of this element.
        /// </summary>
        /// <param name="element">The element you want to add. Must implement IHtmlComponent</param>
        /// <returns>this - The anker object itself</returns>
        public A Add(IHtmlComponent element)
        {
            this.content.Add(element);
            return this;
        }

        public A Href(string href)
        {
            this.href = href;
            return this;
        }

        /// <summary>
        /// Add a class attribute
        /// </summary>
        /// <param name="cssClass">The name of the css class you want to assign.</param>
        /// <returns>this - The anker object itself</returns>
        public A Class(string cssClass)
        {
            this.cssClass = cssClass;
            return this;
        }

        /// <summary>
        /// Add a style attribute
        /// </summary>
        /// <param name="style">The content of the style attribute.</param>
        /// <returns>this - The anker object itself></returns>
        public A Style(string style)
        {
            this.cssStyle = style;
            return this;
        }


        public string Render()
        {
            StringBuilder componentBuilder = new();

            // Build leading tag
            componentBuilder.Append("<a");

            // Add href
            componentBuilder.Append($" href=\"{this.href}\"");

            // Add classes
            if (cssClass is not null)
            {
                componentBuilder.Append($" class=\"{this.cssClass}\"");
            }

            // Add styles
            if (cssStyle is not null)
            {
                componentBuilder.Append($" style=\"{this.cssStyle}\"");
            }
            // Close leading tag
            componentBuilder.Append(">");

            // Build body of anker element
            foreach (IHtmlComponent element in this.content)
            {
                componentBuilder.Append(element.Render());
            }

            // Build trailing tag
            componentBuilder.Append("</a>");

            return componentBuilder.ToString();
        }
    }
}
