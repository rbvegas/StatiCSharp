using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatiCsharp.HtmlComponents;

namespace StatiCsharp
{
    /// <summary>
    /// A representation of a HTML document.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    public class HTML : IHtmlComponent
    {
        // Contains the components inside the html-component
        private List<IHtmlComponent> content;

        /// <summary>
        /// Initiate a new empty html element.
        /// </summary>
        public HTML()
        {
            this.content = new List<IHtmlComponent>();
        }

        /// <summary>
        /// Initiate a new html element.
        /// </summary>
        /// <param name="element">The element inside the body of the html tag.</param>
        public HTML(IHtmlComponent element)
        {
            this.content = new List<IHtmlComponent>();
            this.content.Add(element);
        }

        /// <summary>
        /// Initiate a new html element.
        /// </summary>
        /// <param name="text">The text inside the body of the html tag.</param>
        public HTML(string text)
        {
            this.content = new List<IHtmlComponent>();
            this.content.Add(new Text(text));
        }

        /// <summary>
        /// Add additional components to the html element.
        /// </summary>
        /// <param name="element">The component you want to add to the body of the html tag.</param>
        /// <returns>this - the html element itself.</returns>
        public HTML Add(IHtmlComponent element)
        {
            this.content.Add(element);
            return this;
        }

        /// <summary>
        /// Renders the component and all its containing elements as a html-code string.
        /// </summary>
        /// <returns>The html-code for the component.</returns>
        public string Render()
        {
            StringBuilder componentBuilder = new StringBuilder("<html>");
            foreach (IHtmlComponent element in this.content)
            {
                componentBuilder.Append(element.Render());
            }
            componentBuilder.Append("</html>");
            return componentBuilder.ToString();
        }
    }

    
}
