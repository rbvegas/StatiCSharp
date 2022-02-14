using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatiCsharp.HtmlComponents
{
    /// <summary>
    /// A representation of a div element.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    internal class Div : IHtmlComponent
    {
        // Contains the components inside the div-container
        private List<IHtmlComponent> content;

        // CSS classes
        private string? cssClass;

        // Styles
        private string? cssStyle;

        /// Initiate a new and empty div-element
        public Div()
        {
            this.content = new List<IHtmlComponent>();
        }

        /// <summary>
        /// Add a new Element to the body if this element
        /// </summary>
        /// <param name="element">The element you want to add. Must implement IHtmlComponent</param>
        /// <returns>this - The div object itself</returns>
        public Div Add(IHtmlComponent element)
        {
            this.content.Add(element);
            return this;
        }

        /// <summary>
        /// Add a class attribute
        /// </summary>
        /// <param name="cssClass">The name of the css class you want to assign.</param>
        /// <returns>this - The div object itself</returns>
        public Div Class(string cssClass)
        {
            this.cssClass = cssClass;
            return this;
        }

        /// <summary>
        /// Add a style attribute
        /// </summary>
        /// <param name="style">The content of the style attribute.</param>
        /// <returns>this - The div object itself></returns>
        public Div Style(string style)
        {
            this.cssStyle = style;
            return this;
        }

        /// <summary>
        /// Renders the div to html code.
        /// </summary>
        /// <returns>A string containing the html code of this div.</returns>
        public string Render()
        {
            StringBuilder componentBuilder = new();

            // Build leading tag
            componentBuilder.Append("<div");
            
            // Add classes
            if (cssClass is not null) {
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
            componentBuilder.Append("</div>");
            return componentBuilder.ToString();
        }
    }
}
