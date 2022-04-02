using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatiCsharp.Interfaces;

namespace StatiCsharp.HtmlComponents
{
    /// <summary>
    /// A representation of a nav element.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    public class Nav : IHtmlComponent
    {
        /// Contains the components inside the nav-container.
        private List<IHtmlComponent> content;

        /// CSS classes
        private string? cssClass;

        /// Styles
        private string? cssStyle;

        /// <summary>
        /// Initiate a new and empty nav-element.
        /// </summary>
        public Nav()
        {
            this.content = new List<IHtmlComponent>();
        }

        /// <summary>
        /// Initiate a new nav element.
        /// </summary>
        /// <param name="component">The component inside the body of the nav tag.</param>
        public Nav(IHtmlComponent component)
        {
            this.content = new List<IHtmlComponent> { component };
        }

        /// <summary>
        /// Initiate a new nav element.
        /// </summary>
        /// <param name="text">The text inside the body of the nav tag.</param>
        public Nav(string text)
        {
            this.content = new List<IHtmlComponent> { new Text(text) };
        }

        /// <summary>
        /// Add a new element to the body of this element.
        /// </summary>
        /// <param name="element">The element you want to add. Must implement IHtmlComponent</param>
        /// <returns>this - The nav object itself</returns>
        public Nav Add(IHtmlComponent element)
        {
            this.content.Add(element);
            return this;
        }

        /// <summary>
        /// Add a class attribute
        /// </summary>
        /// <param name="cssClass">The name of the css class you want to assign.</param>
        /// <returns>this - The nav object itself</returns>
        public Nav Class(string cssClass)
        {
            this.cssClass = cssClass;
            return this;
        }

        /// <summary>
        /// Add a style attribute
        /// </summary>
        /// <param name="style">The content of the style attribute.</param>
        /// <returns>this - The nav object itself></returns>
        public Nav Style(string style)
        {
            this.cssStyle = style;
            return this;
        }

        /// <summary>
        /// Renders the nav to html code.
        /// </summary>
        /// <returns>A string containing the html code of this nav.</returns>
        public string Render()
        {
            StringBuilder componentBuilder = new();

            // Build leading tag
            componentBuilder.Append("<nav");

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


            // Build body of div element
            foreach (IHtmlComponent element in this.content)
            {
                componentBuilder.Append(element.Render());
            }

            // Build trailing tag
            componentBuilder.Append("</nav>");

            return componentBuilder.ToString();
        }
    }
}
