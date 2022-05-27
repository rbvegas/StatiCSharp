using StatiCsharp.Interfaces;
using System.Text;

namespace StatiCsharp.HtmlComponents
{
    /// <summary>
    /// A representation of a <li></li> element.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    public class Li: IHtmlComponent
    {
        /// <summary>
        /// Contains the components inside the li-container.
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
        /// Only for <ol> lists. Specifies the start value of a list item. The following list items will increment from that number.
        /// </summary>
        private string? value;

        /// <summary>
        /// Initiate a new empty li element.
        /// </summary>
        public Li()
        {
            this.content = new List<IHtmlComponent>();
        }

        /// <summary>
        /// Initiate a new li element.
        /// </summary>
        /// <param name="component">The component for the content of the li.</param>
        public Li(IHtmlComponent component)
        {
            this.content = new List<IHtmlComponent> { component };
        }

        /// <summary>
        /// Initiate a new li element.
        /// </summary>
        /// <param name="text">The text inside the body of the li.</param>
        public Li(string text)
        {
            this.content = new List<IHtmlComponent> { new Text(text) };
        }

        /// <summary>
        /// Add a new component to the body of this element.
        /// </summary>
        /// <param name="component">The component you want to add.</param>
        /// <returns>this - the object itself</returns>
        public Li Add(IHtmlComponent component)
        {
            content.Add(component);
            return this;
        }

        /// <summary>
        /// Add a class attribute
        /// </summary>
        /// <param name="cssClass">The name of the css class you want to assign.</param>
        /// <returns>this - The object itself</returns>
        public Li Class(string cssClass)
        {
            this.cssClass = cssClass;
            return this;
        }

        /// <summary>
        /// Add a style attribute
        /// </summary>
        /// <param name="style">The content of the style attribute.</param>
        /// <returns>this - The object itself></returns>
        public Li Style(string style)
        {
            this.cssStyle = style;
            return this;
        }

        /// <summary>
        /// Only for <ol> lists. Specifies the start value of a list item. The following list items will increment from that number
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Li Value(string value)
        {
            this.value = value;
            return this;
        }

        /// <summary>
        /// Renders the li to html code.
        /// </summary>
        /// <returns>A string containing the html code of this li.</returns>
        public string Render()
        {
            StringBuilder componentBuilder = new();

            // Build leading tag
            componentBuilder.Append("<li");

            // Add classes
            if (this.cssClass is not null)
            {
                componentBuilder.Append($" class=\"{this.cssClass}\"");
            }

            // Add styles
            if (this.cssStyle is not null)
            {
                componentBuilder.Append($" style=\"{this.cssStyle}\"");
            }

            // Add value
            if (this.value is not null)
            {
                componentBuilder.Append($" value=\"{this.value}\"");
            }

            // Close leading tag
            componentBuilder.Append(">");


            // Build body of div element
            foreach (IHtmlComponent element in this.content)
            {
                componentBuilder.Append(element.Render());
            }

            // Build trailing tag
            componentBuilder.Append("</li>");

            return componentBuilder.ToString();
        }
    }
}
