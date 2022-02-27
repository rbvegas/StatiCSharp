using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatiCsharp.Interfaces;

namespace StatiCsharp
{
    public partial class Website: IWebsite
    {
        public void MakeTagLists(IHtmlFactory htmlFactory)
        {
            // Collect all available tags
            List<string> tags = new List<string>();
            foreach (ISection currentSection  in this.Sections)
            {
                foreach (IItem currentItem in currentSection.Items)
                {
                    foreach (string tag in currentItem.Tags)
                    {
                        // Check if tag is already in list. If not, add it
                        if (!tags.Contains(tag)) { tags.Add(tag); }
                    }
                }
            }

            // Make a taglist for each tag
            foreach (string tag in tags)
            {
                List<IItem> itemsWithCurrentTag = new();
                // Collect all items with the current tag
                foreach (ISection currentSection in this.Sections)
                {
                    foreach (IItem item in currentSection.Items)
                    {
                        if (item.Tags.Contains(tag))
                        {
                            itemsWithCurrentTag.Add(item);
                        }
                    }
                }
                // Write tags sites to files
                IItem tagPage = new Item();
                tagPage.Title = $"{tag} | {this.Name}";
                string body = htmlFactory.MakeTagListHtml(itemsWithCurrentTag, tag);
                string page = AddLeadingHtmlCode(this, tagPage, body);

                // Create directory, if it does not excist
                string path = Directory.CreateDirectory(Path.Combine(output, "tag", tag)).ToString();
                File.WriteAllText(Path.Combine(path, "index.html"), page);
            }
        }
    }
}