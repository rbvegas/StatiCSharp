using StatiCSharp.Interfaces;

namespace StatiCSharp.HtmlComponents
{
    /// <summary>
    /// A representation of text.
    /// Call the Render() method to turn it into an HTML string.
    /// </summary>
    public class Text: IHtmlComponent
    {
        // Contains the text as a string
        private string _Text { get; set; }

        /// <summary>
        /// Initiate an empty Text.
        /// </summary>
        public Text()
        {
            _Text = string.Empty;
        }

        /// <summary>
        /// Initiate a new text.
        /// </summary>
        /// <param name="text">Your text.</param>
        public Text(string text)
        {
            _Text = text;
        }

        public Text Add(string text)
        {
            _Text = text;
            return this;
        }

        public string Render()
        {
            return _Text;
        }
    }
}
