using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatiCsharp
{
    /// <summary>
    /// A representation of a HTML document.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    internal class HTML : IHtmlComponent
    {
        // Contains the components inside the html-component
        List<IHtmlComponent> content;

        public HTML()
        {
            this.content = new List<IHtmlComponent>();
        }

        public HTML Add(IHtmlComponent element)
        {
            this.content.Add(element);
            return this;
        }

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
