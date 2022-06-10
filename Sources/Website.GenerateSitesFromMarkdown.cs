using Markdig;
using StatiCsharp.Interfaces;

namespace StatiCsharp
{
    public partial class Website : IWebsite
    {
        /// <summary>
        /// Generates index, pages, sections and items from the markdown files in the `Content` directory.
        /// </summary>
        /// <param name="website">The website where the parsed sites are added to.</param>
        private void GenerateSitesFromMarkdown(IWebsite website)
        {
            string pathToContent = website.Content;

            // For the index: Collecting meta data and content from markdown files.
            Dictionary<string, string> mdMetaData = MarkdownFactory.ParseMetaData(Path.Combine(pathToContent, "index.md"));
            string mdContent = MarkdownFactory.ParseContent(Path.Combine(pathToContent, "index.md"));
            website.Index.Content = Markdown.ToHtml(MarkdownFactory.ParseContent(Path.Combine(pathToContent, "index.md")));
            string MarkdownFilePath = Path.GetFileName(Path.Combine(pathToContent, "index.md"));
            website.Index.MarkdownFileName = MarkdownFilePath.Substring(0, MarkdownFilePath.LastIndexOf(".md"));
            website.Index.MarkdownFilePath = Path.Combine(pathToContent, "index.md").ToString();
            MapMetaData(mdMetaData, website.Index);

            // Collecting pages and sections data
            string[] directoriesOfContent = Directory.GetDirectories(pathToContent);
            foreach (string directory in directoriesOfContent)
            {
                string nameOfCurrentDirectory = Path.GetFileName(directory);
                if (!website.MakeSectionsFor.Contains(nameOfCurrentDirectory))
                {
                    // Add a page
                    string[] filenames = Directory.GetFiles(directory, "*.md");
                    for (int i = 0; i < filenames.Length; i++)
                    {
                        filenames[i] = Path.GetFileName(filenames[i]);
                    }

                    if (filenames.Length > 0)
                    {
                        foreach (string filename in filenames)
                        {
                            IPage currentPage = new Page();
                            MapMetaData(MarkdownFactory.ParseMetaData(Path.Combine(pathToContent, directory, filename)), currentPage);
                            currentPage.Content = Markdown.ToHtml(MarkdownFactory.ParseContent(Path.Combine(pathToContent, directory, filename)));

                            currentPage.MarkdownFileName = filename;
                            currentPage.MarkdownFilePath = Path.Combine(directory, filename).ToString();
                            currentPage.Hierarchy = nameOfCurrentDirectory;
                            website.Pages.Add(currentPage);
                        }
                    }
                }
                else
                {
                    // Add a section
                    ISection currentSection = new Section();
                    currentSection.SectionName = nameOfCurrentDirectory;

                    string[] filenames = Directory.GetFiles(directory, "*.md");
                    for (int i = 0; i < filenames.Length; i++)
                    {
                        filenames[i] = Path.GetFileName(filenames[i]);
                    }

                    if (filenames.Length > 0)
                    {
                        foreach (string filename in filenames)
                        {
                            // If filename == index.md the content is for the section itself. Otherwise an item is generated.
                            if (filename == "index.md")
                            {
                                // Add content to the section itself
                                MapMetaData(MarkdownFactory.ParseMetaData(Path.Combine(pathToContent, directory, filename)), currentSection);
                                currentSection.Content = Markdown.ToHtml(MarkdownFactory.ParseContent(Path.Combine(pathToContent, directory, filename)));

                                currentSection.MarkdownFileName = filename;
                                currentSection.MarkdownFilePath = Path.Combine(directory,filename).ToString();  
                            }
                            else
                            {
                                // Add item
                                Item currentItem = new Item();
                                Dictionary<string, string> currentMetaData = MarkdownFactory.ParseMetaData(Path.Combine(pathToContent, directory, filename));
                                MapMetaData(MarkdownFactory.ParseMetaData(Path.Combine(pathToContent, directory, filename)), currentItem);
                                currentItem.Content = Markdown.ToHtml(MarkdownFactory.ParseContent(Path.Combine(pathToContent, directory, filename)));

                                currentItem.DateLastModified = DateOnly.FromDateTime(Directory.GetLastWriteTime(Path.Combine(pathToContent, directory, filename)));

                                if (currentMetaData.ContainsKey("date"))
                                {
                                    if (currentMetaData["date"] == string.Empty)
                                    {
                                        currentMetaData["date"] = currentItem.DateLastModified.ToString();
                                    }
                                }
                                else
                                {
                                    currentMetaData.Add("date", currentItem.DateLastModified.ToString());
                                }

                                currentItem.MarkdownFileName = filename;
                                currentItem.MarkdownFilePath = Path.Combine(directory, filename).ToString();
                                currentItem.Section = nameOfCurrentDirectory;
                                currentSection.AddItem(currentItem);
                            }

                        }

                        website.Sections.Add(currentSection);
                    }
                }
            }
        }

    }
}
