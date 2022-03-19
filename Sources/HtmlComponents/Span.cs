using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatiCsharp.Interfaces;

namespace StatiCsharp.HtmlComponents
{
    /// <summary>
    /// A representation of a span element.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    public class Span : IHtmlComponent
    {
        /// <summary>
        /// Contains the components inside the span-container.
        /// </summary>
        private List<IHtmlComponent> content;

        /// CSS classes
        private string? cssClass;

        /// Styles
        private string? cssStyle;

        /// <summary>
        /// Initiate a new empty span element.
        /// </summary>
        public Span()
        {
            this.content = new List<IHtmlComponent>();
        }

        /// <summary>
        /// Initiate a new span element.
        /// </summary>
        /// <param name="component">The component for the content of the span.</param>
        public Span(IHtmlComponent component)
        {
            this.content = new List<IHtmlComponent>() { component };
        }

        /// <summary>
        /// Initiate a new span element.
        /// </summary>
        /// <param name="text">The text inside the body of the span.</param>
        public Span(string text)
        {
            this.content = new List<IHtmlComponent>() { new Text(text) };
        }

        /// <summary>
        /// Add a new element to the body of this element.
        /// </summary>
        /// <param name="component">The element you want to add. Must implement IHtmlComponent</param>
        /// <returns>this - The span object itself</returns>
        public Span Add(IHtmlComponent component)
        {
            this.content.Add(component);
            return this;
        }

        /// <summary>
        /// Add a class attribute
        /// </summary>
        /// <param name="cssClass">The name of the css class you want to assign.</param>
        /// <returns>this - The span object itself</returns>
        public Span Class(string cssClass)
        {
            this.cssClass = cssClass;
            return this;
        }

        /// <summary>
        /// Add a style attribute
        /// </summary>
        /// <param name="style">The content of the style attribute.</param>
        /// <returns>this - The span object itself></returns>
        public Span Style(string style)
        {
            this.cssStyle = style;
            return this;
        }

        /// <summary>
        /// Renders the span to html code.
        /// </summary>
        /// <returns>A string containing the html code of this span.</returns>
        public string Render()
        {
            StringBuilder componentBuilder = new();

            // Build leading tag
            componentBuilder.Append("<span");
            
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


            // Build body of span element
            foreach (IHtmlComponent element in this.content)
            {
                componentBuilder.Append(element.Render());
            }

            // Build trailing tag
            componentBuilder.Append("</span>");

            return componentBuilder.ToString();
        }
    }
}
