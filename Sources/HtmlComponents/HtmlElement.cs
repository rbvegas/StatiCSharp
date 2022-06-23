using StatiCsharp.Interfaces;
using System.Text;

namespace StatiCsharp.HtmlComponents
{
    /// <summary>
    /// A base class for all basuc HTML elements.
    /// </summary>
    public abstract class HtmlElement : IHtmlComponent
    {
        /// <summary>
        /// Defines the tagname of the element.
        /// </summary>
        private protected abstract string TagName { get; }
        
        /// <summary>
        /// Contains the components inside the element.
        /// </summary>
        private protected List<IHtmlComponent> Content { get; set; }


        private Dictionary<string, string?> _attributes = new Dictionary<string, string?>();

        /// <summary>
        /// Contains the attributes that are added to the opening tag of the element.
        /// <Key><Value> is equivalent to Key="Value".
        /// </summary>
        private protected Dictionary<string, string?> Attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }


        private protected bool _voidElement = false;
        /// <summary>
        /// If this element is a void element.
        /// A void element is an element whose content model never allows it to have contents under any circumstances.
        /// Void elements can have attributes.
        /// Void elements only have a start tag. Closing tags must not be specified for void elements.
        /// </summary>
        private protected virtual bool VoidElement
        {
            get { return _voidElement; }
            set { _voidElement = value; }
        }

        public HtmlElement()
        {
            Content = new List<IHtmlComponent>();
        }

        /// <summary>
        /// Add a new element or component to the content of this element.
        /// </summary>
        /// <param name="component">The element or component you want to add. Must implement IHtmlComponent</param>
        /// <returns>his - The element object itself</returns>
        public HtmlElement Add(IHtmlComponent component)
        {
            Content.Add(component);
            return this;
        }

        /// <summary>
        /// Add a class attribute.
        /// </summary>
        /// <param name="cssClass">The name of the css class you want to assign.</param>
        /// <returns>this - the element itself.</returns>
        public HtmlElement Class(string cssClass)
        {
            Attributes.Add("class", cssClass);
            return this;
        }

        /// <summary>
        /// Add a style attribute.
        /// </summary>
        /// <param name="style">The content of the style attribute.</param>
        /// <returns>this - The element itself.></returns>
        public HtmlElement Style(string style)
        {
            Attributes.Add("style", style);
            return this;
        }

        /// <summary>
        /// Renders the element to html code.
        /// </summary>
        /// <returns>A string containing the html code of this element</returns>
        public virtual string Render()
        {
            StringBuilder elementBuilder = new();

            // Build leading tag
            elementBuilder.Append($"<{TagName}");

            // Add attributes with key-value pairs
            foreach (KeyValuePair<string, string?> attribute in Attributes)
            {
                if (!string.IsNullOrEmpty(attribute.Key) && (!string.IsNullOrEmpty(attribute.Value)))
                {
                    elementBuilder.Append($" {attribute.Key}=\"{attribute.Value!}\"");
                }
            }

            // Add attributes with just keys
            foreach (KeyValuePair<string, string?> attribute in Attributes)
            {
                if ((!string.IsNullOrEmpty(attribute.Key)) && (attribute.Value is null))
                {
                    elementBuilder.Append($" {attribute.Key}");
                }
            }

            // Close leading tag
            elementBuilder.Append(">");

            // Build content of the element
            if (!VoidElement)
            {
                foreach (IHtmlComponent element in Content)
                {
                    elementBuilder.Append(element.Render());
                }

                // Build trailing tag
                elementBuilder.Append($"</{TagName}>");
            }

            return elementBuilder.ToString();
        }
    }
}
