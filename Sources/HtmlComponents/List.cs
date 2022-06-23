using StatiCsharp.Interfaces;

namespace StatiCsharp.HtmlComponents
{
    /// <summary>
    /// A representation of a <li></li> element.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    public class Li: HtmlElement, IHtmlComponent
    {
        private protected override string TagName
        {
            get { return "li"; }
        }

        /// <summary>
        /// Initiate a new empty li element.
        /// </summary>
        public Li()
        {
            // No action needed, because the base class already initialized an empty List<IHtmlComponent>.
        }

        /// <summary>
        /// Initiate a new li element with another element or component inside.
        /// </summary>
        /// <param name="component">The component for the content of the li.</param>
        public Li(IHtmlComponent component)
        {
            Content = new List<IHtmlComponent> { component };
        }

        /// <summary>
        /// Initiate a new li element with text.
        /// </summary>
        /// <param name="text">The text inside the li element.</param>
        public Li(string text)
        {
            Content = new List<IHtmlComponent> { new Text(text) };
        }

        /// <summary>
        /// Only for <ol> lists. Specifies the start value of a list item. The following list items will increment from that number
        /// </summary>
        /// <param name="value"></param>
        /// <returns>this - the element itself.</returns>
        public Li Value(string value)
        {
            if (!Attributes.TryAdd("value", value))
            {
                Attributes["value"] = value;
            }
            return this;
        }

    }


    /// <summary>
    /// A representation of a <ul></ul> element.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    public class Ul : HtmlElement, IHtmlComponent
    {
        private protected override string TagName
        {
            get { return "ul"; }
        }

        /// <summary>
        /// Initiate a new empty <ul> element.
        /// </summary>
        public Ul()
        {
            // No action needed, because the base class already initialized an empty List<IHtmlComponent>.
        }

        /// <summary>
        /// Initiate a new <ul> element with another element or component inside.
        /// </summary>
        /// <param name="component">The component for the content of the ul.</param>
        public Ul(IHtmlComponent component)
        {
            Content = new List<IHtmlComponent> { component };
        }

        /// <summary>
        /// Initiate a new ul element with text.
        /// </summary>
        /// <param name="text">The text for the content of the <ul>.</param>
        public Ul(string text)
        {
            Content = new List<IHtmlComponent> { new Text(text) };
        }

    }


    /// <summary>
    /// A representation of a <ol></ol> element.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    public class Ol : HtmlElement, IHtmlComponent
    {
        private protected override string TagName
        {
            get { return "ol"; }
        }

        /// <summary>
        /// Initiate a new empty <ol> element.
        /// </summary>
        public Ol()
        {
            // No action needed, because the base class already initialized an empty List<IHtmlComponent>.
        }

        /// <summary>
        /// Initiate a new <ol> element with another element or component inside.
        /// </summary>
        /// <param name="component">The component for the content of the ol.</param>
        public Ol(IHtmlComponent component)
        {
            Content = new List<IHtmlComponent> { component };
        }

        /// <summary>
        /// Initiate a new ol element with text.
        /// </summary>
        /// <param name="text">The text for the content of the <ol>.</param>
        public Ol(string text)
        {
            Content = new List<IHtmlComponent> { new Text(text) };
        }

        /// <summary>
        /// The type attribute of the <ol> tag, defines the type of the list item marker:
        /// type="1"	The list items will be numbered with numbers (default)
        /// type="A"	The list items will be numbered with uppercase letters
        /// type="a"	The list items will be numbered with lowercase letters
        /// type="I"	The list items will be numbered with uppercase roman numbers
        /// type="i"	The list items will be numbered with lowercase roman numbers
        /// </summary>
        /// <param name="t">The type</param>
        /// <returns>this - the element itself.</returns>
        public Ol Type(char t)
        {
            if (!Attributes.TryAdd("type", t.ToString()))
            {
                Attributes["type"] = t.ToString();
            }
            return this;
        }

        /// <summary>
        /// The type attribute of the <ol> tag, defines the type of the list item marker:
        /// type="1"	The list items will be numbered with numbers (default)
        /// type="A"	The list items will be numbered with uppercase letters
        /// type="a"	The list items will be numbered with lowercase letters
        /// type="I"	The list items will be numbered with uppercase roman numbers
        /// type="i"	The list items will be numbered with lowercase roman numbers
        /// </summary>
        /// <param name="t">The type</param>
        /// <returns>this - the element itself.</returns>
        public Ol Type(int t)
        {
            if (!Attributes.TryAdd("type", t.ToString()))
            {
                Attributes["type"] = t.ToString();
            }
            return this;
        }

    }
}
