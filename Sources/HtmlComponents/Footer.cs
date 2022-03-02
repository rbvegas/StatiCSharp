using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatiCsharp.HtmlComponents
{
    /// <summary>
    /// A representation of a <footer></footer> element.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    public class Footer : IHtmlComponent
    {
        /// <summary>
        /// Contains the components inside the <footer> element.
        /// </summary>
        private List<IHtmlComponent> content;

        /// CSS classes
        private string? cssClass;

        /// Styles
        private string? cssStyle;

        /// <summary>
        /// Initiate a new empty <footer> element.
        /// </summary>
        public Footer()
        {
            this.content = new List<IHtmlComponent>();
        }

        /// <summary>
        /// Initiate a new <footer> element.
        /// </summary>
        /// <param name="component">The component for the body of the <footer>.</param>
        public Footer(IHtmlComponent component)
        {
            this.content = new List<IHtmlComponent>() { component };
        }

        /// <summary>
        /// Initiate a new <footer> element.
        /// </summary>
        /// <param name="text">The text inside the body of the <footer>.</param>
        public Footer(string text)
        {
            this.content = new List<IHtmlComponent>() { new Text(text) };
        }

        /// <summary>
        /// Add a new element to the body of this element.
        /// </summary>
        /// <param name="component">The element you want to add. Must implement IHtmlComponent.</param>
        /// <returns>this - the <footer> object itself.</returns>
        public Footer Add(IHtmlComponent component)
        {
            this.content.Add(component);
            return this;
        }

        /// <summary>
        /// Add text to the body of this element.
        /// </summary>
        /// <param name="text">The text you want to add.</param>
        /// <returns>this - the <footer> object itself.</returns>
        public Footer Add(string text)
        {
            this.content.Add(new Text(text));
            return this;
        }

        /// <summary>
        /// Add a class attribute.
        /// </summary>
        /// <param name="cssClass">The name of the css class you want to assign.</param>
        /// <returns>this - the <footer> object itself.</returns>
        public Footer Class(string cssClass)
        {
            this.cssClass = cssClass;
            return this;
        }

        /// <summary>
        /// Add a style attribute.
        /// </summary>
        /// <param name="style">The content of the style attribute.</param>
        /// <returns>this - the <footer> object itself.></returns>
        public Footer Style(string style)
        {
            this.cssStyle = style;
            return this;
        }

        /// <summary>
        /// Renders the <footer> to html code.
        /// </summary>
        /// <returns>A string containing the html code of this <footer>.</returns>
        public string Render()
        {
            StringBuilder componentBuilder = new();

            // Build leading tag
            componentBuilder.Append("<footer");
            
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
            componentBuilder.Append("</footer>");

            return componentBuilder.ToString();
        }
    }
}
