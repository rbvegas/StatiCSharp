using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatiCsharp.HtmlComponents
{
    /// <summary>
    /// A representation of a <ul></ul> element.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    public class Ul:IHtmlComponent
    {
        /// <summary>
        /// Contains the components inside the ul-container.
        /// </summary>
        private List<IHtmlComponent> content;

        /// <summary>
        /// CSS classes
        /// </summary>
        private string? cssClass;

        /// <summary>
        /// Styles
        /// </summary>
        private string? cssStyle;

        /// <summary>
        /// Initiate a new empty ul element.
        /// </summary>
        public Ul()
        {
            this.content = new List<IHtmlComponent>();
        }

        /// <summary>
        /// Initiate a new ul element.
        /// </summary>
        /// <param name="component">The component for the content of the ul.</param>
        public Ul(IHtmlComponent component)
        {
            this.content = new List<IHtmlComponent> { component };
        }

        /// <summary>
        /// Initiate a new ul element.
        /// </summary>
        /// <param name="text">The text inside the body of the ul.</param>
        public Ul(string text)
        {
            this.content = new List<IHtmlComponent> { new Text(text) };
        }

        /// <summary>
        /// Add a new component to the body of this element.
        /// </summary>
        /// <param name="component">The component you want to add.</param>
        /// <returns>this - the ul object itself</returns>
        public Ul Add(IHtmlComponent component)
        {
            content.Add(component);
            return this;
        }

        /// <summary>
        /// Add a class attribute
        /// </summary>
        /// <param name="cssClass">The name of the css class you want to assign.</param>
        /// <returns>this - The ul object itself</returns>
        public Ul Class(string cssClass)
        {
            this.cssClass = cssClass;
            return this;
        }

        /// <summary>
        /// Add a style attribute
        /// </summary>
        /// <param name="style">The content of the style attribute.</param>
        /// <returns>this - The ul object itself></returns>
        public Ul Style(string style)
        {
            this.cssStyle = style;
            return this;
        }

        /// <summary>
        /// Renders the ul to html code.
        /// </summary>
        /// <returns>A string containing the html code of this ul.</returns>
        public string Render()
        {
            StringBuilder componentBuilder = new();

            // Build leading tag
            componentBuilder.Append("<ul");

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
            componentBuilder.Append("</ul>");

            return componentBuilder.ToString();
        }
    }
}
