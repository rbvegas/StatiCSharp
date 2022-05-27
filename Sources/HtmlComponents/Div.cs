using StatiCsharp.Interfaces;
using System.Text;

namespace StatiCsharp.HtmlComponents
{
    /// <summary>
    /// A representation of a div element.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    public class Div : IHtmlComponent
    {
        /// <summary>
        /// Contains the components inside the div-container.
        /// </summary>
        private List<IHtmlComponent> content;

        /// CSS classes
        private string? cssClass;

        /// Styles
        private string? cssStyle;

        /// <summary>
        /// Initiate a new empty div element.
        /// </summary>
        public Div()
        {
            this.content = new List<IHtmlComponent>();
        }

        /// <summary>
        /// Initiate a new div element.
        /// </summary>
        /// <param name="component">The component for the content of the div.</param>
        public Div(IHtmlComponent component)
        {
            this.content = new List<IHtmlComponent>() { component };
        }

        /// <summary>
        /// Initiate a new div element.
        /// </summary>
        /// <param name="text">The text inside the body of the div.</param>
        public Div(string text)
        {
            this.content = new List<IHtmlComponent>() { new Text(text) };
        }

        /// <summary>
        /// Add a new element to the body of this element.
        /// </summary>
        /// <param name="component">The element you want to add. Must implement IHtmlComponent</param>
        /// <returns>this - The div object itself</returns>
        public Div Add(IHtmlComponent component)
        {
            this.content.Add(component);
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
