using StatiCsharp.Interfaces;

namespace StatiCsharp.HtmlComponents
{
    /// <summary>
    /// A representation of a <h1></h1> element.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    public class H1 : HtmlElement, IHtmlComponent
    {
        private protected override string TagName
        {
            get { return "h1"; }
        }

        /// <summary>
        /// Initiate a new empty element.
        /// </summary>
        public H1()
        {
            // No action needed, because the base class already initialized an empty List<IHtmlComponent>.
        }

        /// <summary>
        /// Initiate a new element with another element or component inside.
        /// </summary>
        /// <param name="component">The element or component for the content of the element.</param>
        public H1(IHtmlComponent component)
        {
            Content = new List<IHtmlComponent>() { component };
        }

        /// <summary>
        /// Initiate a new element with text.
        /// </summary>
        /// <param name="text">The text for the content of the element.</param>
        public H1(string text)
        {
            Content = new List<IHtmlComponent>() { new Text(text) };
        }
    }


    /// <summary>
    /// A representation of a <h2></h2> element.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    public class H2 : HtmlElement, IHtmlComponent
    {
        private protected override string TagName
        {
            get { return "h2"; }
        }

        /// <summary>
        /// Initiate a new empty element.
        /// </summary>
        public H2()
        {
            // No action needed, because the base class already initialized an empty List<IHtmlComponent>.
        }

        /// <summary>
        /// Initiate a new element with another element or component inside.
        /// </summary>
        /// <param name="component">The element or component for the content of the element.</param>
        public H2(IHtmlComponent component)
        {
            Content = new List<IHtmlComponent>() { component };
        }

        /// <summary>
        /// Initiate a new element with text.
        /// </summary>
        /// <param name="text">The text for the content of the element.</param>
        public H2(string text)
        {
            Content = new List<IHtmlComponent>() { new Text(text) };
        }
    }


    /// <summary>
    /// A representation of a <h3></h3> element.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    public class H3 : HtmlElement, IHtmlComponent
    {
        private protected override string TagName
        {
            get { return "h3"; }
        }

        /// <summary>
        /// Initiate a new empty element.
        /// </summary>
        public H3()
        {
            // No action needed, because the base class already initialized an empty List<IHtmlComponent>.
        }

        /// <summary>
        /// Initiate a new element with another element or component inside.
        /// </summary>
        /// <param name="component">The element or component for the content of the element.</param>
        public H3(IHtmlComponent component)
        {
            Content = new List<IHtmlComponent>() { component };
        }

        /// <summary>
        /// Initiate a new element with text.
        /// </summary>
        /// <param name="text">The text for the content of the element.</param>
        public H3(string text)
        {
            Content = new List<IHtmlComponent>() { new Text(text) };
        }
    }


    /// <summary>
    /// A representation of a <h4></h4> element.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    public class H4 : HtmlElement, IHtmlComponent
    {
        private protected override string TagName
        {
            get { return "h4"; }
        }

        /// <summary>
        /// Initiate a new empty element.
        /// </summary>
        public H4()
        {
            // No action needed, because the base class already initialized an empty List<IHtmlComponent>.
        }

        /// <summary>
        /// Initiate a new element with another element or component inside.
        /// </summary>
        /// <param name="component">The element or component for the content of the element.</param>
        public H4(IHtmlComponent component)
        {
            Content = new List<IHtmlComponent>() { component };
        }

        /// <summary>
        /// Initiate a new element with text.
        /// </summary>
        /// <param name="text">The text for the content of the element.</param>
        public H4(string text)
        {
            Content = new List<IHtmlComponent>() { new Text(text) };
        }
    }


    /// <summary>
    /// A representation of a <h5></h5> element.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    public class H5 : HtmlElement, IHtmlComponent
    {
        private protected override string TagName
        {
            get { return "h5"; }
        }

        /// <summary>
        /// Initiate a new empty element.
        /// </summary>
        public H5()
        {
            // No action needed, because the base class already initialized an empty List<IHtmlComponent>.
        }

        /// <summary>
        /// Initiate a new element with another element or component inside.
        /// </summary>
        /// <param name="component">The element or component for the content of the element.</param>
        public H5(IHtmlComponent component)
        {
            Content = new List<IHtmlComponent>() { component };
        }

        /// <summary>
        /// Initiate a new element with text.
        /// </summary>
        /// <param name="text">The text for the content of the element.</param>
        public H5(string text)
        {
            Content = new List<IHtmlComponent>() { new Text(text) };
        }
    }


    /// <summary>
    /// A representation of a <h6></h6> element.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    public class H6 : HtmlElement, IHtmlComponent
    {
        private protected override string TagName
        {
            get { return "h6"; }
        }

        /// <summary>
        /// Initiate a new empty element.
        /// </summary>
        public H6()
        {
            // No action needed, because the base class already initialized an empty List<IHtmlComponent>.
        }

        /// <summary>
        /// Initiate a new element with another element or component inside.
        /// </summary>
        /// <param name="component">The element or component for the content of the element.</param>
        public H6(IHtmlComponent component)
        {
            Content = new List<IHtmlComponent>() { component };
        }

        /// <summary>
        /// Initiate a new element with text.
        /// </summary>
        /// <param name="text">The text for the content of the element.</param>
        public H6(string text)
        {
            Content = new List<IHtmlComponent>() { new Text(text) };
        }
    }
}
