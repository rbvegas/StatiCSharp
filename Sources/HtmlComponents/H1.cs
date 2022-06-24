using StatiCsharp.Interfaces;
using System.Text;

namespace StatiCsharp.HtmlComponents
{
    /// <summary>
    /// A representation of a <h1></h1> element.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    public class H1 : IHtmlComponent
    {
        /// <summary>
        /// Contains the components inside the <h1> element.
        /// </summary>
        private List<IHtmlComponent> content;

        /// CSS classes
        private string? cssClass;

        /// Styles
        private string? cssStyle;

        /// <summary>
        /// Initiate a new empty <h1> element.
        /// </summary>
        public H1()
        {
            this.content = new List<IHtmlComponent>();
        }

        /// <summary>
        /// Initiate a new <h1> element.
        /// </summary>
        /// <param name="component">The component for the body of the <h1>.</param>
        public H1(IHtmlComponent component)
        {
            this.content = new List<IHtmlComponent>() { component };
        }

        /// <summary>
        /// Initiate a new <h1> element.
        /// </summary>
        /// <param name="text">The text inside the body of the <h1>.</param>
        public H1(string text)
        {
            this.content = new List<IHtmlComponent>() { new Text(text) };
        }

        /// <summary>
        /// Add a new element to the body of this element.
        /// </summary>
        /// <param name="component">The element you want to add. Must implement IHtmlComponent.</param>
        /// <returns>this - the <h1> object itself.</returns>
        public H1 Add(IHtmlComponent component)
        {
            this.content.Add(component);
            return this;
        }

        /// <summary>
        /// Add text to the body of this element.
        /// </summary>
        /// <param name="text">The text you want to add.</param>
        /// <returns>this - the <h1> object itself.</returns>
        public H1 Add(string text)
        {
            this.content.Add(new Text(text));
            return this;
        }

        /// <summary>
        /// Add a class attribute.
        /// </summary>
        /// <param name="cssClass">The name of the css class you want to assign.</param>
        /// <returns>this - the <h1> object itself.</returns>
        public H1 Class(string cssClass)
        {
            this.cssClass = cssClass;
            return this;
        }

        /// <summary>
        /// Add a style attribute.
        /// </summary>
        /// <param name="style">The content of the style attribute.</param>
        /// <returns>this - the <h1> object itself.></returns>
        public H1 Style(string style)
        {
            this.cssStyle = style;
            return this;
        }

        /// <summary>
        /// Renders the <h1> to html code.
        /// </summary>
        /// <returns>A string containing the html code of this <h1>.</returns>
        public string Render()
        {
            StringBuilder componentBuilder = new();

            // Build leading tag
            componentBuilder.Append("<h1");
            
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
            componentBuilder.Append("</h1>");

            return componentBuilder.ToString();
        }
    }
}