using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatiCsharp.Interfaces;

namespace StatiCsharp.HtmlComponents
{
    /// <summary>
    /// A representation of a <header></header> element.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    public class Header: IHtmlComponent
    {
        /// Contains the components inside the header-container.
        private List<IHtmlComponent> content;

        /// CSS classes
        private string? cssClass;

        /// Styles
        private string? cssStyle;

        /// <summary>
        /// Initiate a new header element.
        /// </summary>
        public Header()
        {
            this.content = new List<IHtmlComponent>();
        }

        /// <summary>
        /// Initiate a new header element.
        /// </summary>
        /// <param name="element">The element for the content of the header.</param>
        public Header(IHtmlComponent element)
        {
            this.content = new List<IHtmlComponent>();
            this.content.Add(element);
        }

        /// <summary>
        /// Initiate a new header with a text-body.
        /// </summary>
        /// <param name="text">The text inside the header.</param>
        public Header(string text)
        {
            this.content= new List<IHtmlComponent>();
            this.content.Add(new Text(text));
        }

        /// <summary>
        /// Add new element to the body of this element.
        /// </summary>
        /// <param name="element">The element you want to add. Must implement IHtmlComponent</param>
        /// <returns>this - The header object itself</returns>
        public Header Add(IHtmlComponent element)
        {
            this.content.Add(element);
            return this;
        }

        /// <summary>
        /// Add a class attribute
        /// </summary>
        /// <param name="cssClass">The name of the css class you want to assign.</param>
        /// <returns>this - The header object itself</returns>
        public Header Class(string cssClass)
        {
            this.cssClass = cssClass;
            return this;
        }

        /// <summary>
        /// Add a style attribute
        /// </summary>
        /// <param name="style">The content of the style attribute.</param>
        /// <returns>this - The header object itself></returns>
        public Header Style(string style)
        {
            this.cssStyle = style;
            return this;
        }

        public string Render()
        {
            StringBuilder componentBuilder = new StringBuilder();

            // Build leading tag
            componentBuilder.Append("<header");

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

            // Build body of header element
            foreach (IHtmlComponent element in this.content)
            {
                componentBuilder.Append(element.Render());
            }

            // Build trailing tag
            componentBuilder.Append("</header>");

            return componentBuilder.ToString();
        }
    }
}
