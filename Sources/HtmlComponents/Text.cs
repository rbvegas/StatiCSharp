using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatiCsharp.HtmlComponents
{
    /// <summary>
    /// A representation of simple text.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    internal class Text: IHtmlComponent
    {
        // Contains the text as a string
        private string text;

        public Text()
        {
            this.text = string.Empty;
        }

        public Text Add(string text)
        {
            this.text = text;
            return this;
        }

        public string Render()
        {
            return this.text;
        }
    }
}
