using Markdig;
using StatiCSharp.Interfaces;

namespace StatiCSharp
{
    public partial class WebsiteManager : IWebsiteManager
    {
        /// <summary>
        /// Generates index, pages, sections and items for the IWebsite object from the markdown files in the `Content` directory.
        /// </summary>
        private void GenerateSitesFromMarkdown()
        {
            string pathToContent = Content;
            var pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .Build();

            // For the index: Collecting meta data and content from markdown files.
            Dictionary<string, string> markdownMetaData = MarkdownFactory.ParseMetaData(Path.Combine(pathToContent, "index.md"));
            string markdownContent = MarkdownFactory.ParseContent(Path.Combine(pathToContent, "index.md"));
            Website.Index.Content = Markdown.ToHtml(markdown: markdownContent, pipeline: pipeline);
            string markdownFilePath = Path.GetFileName(Path.Combine(pathToContent, "index.md"));
            Website.Index.MarkdownFileName = markdownFilePath.Substring(0, markdownFilePath.LastIndexOf(".md"));
            Website.Index.MarkdownFilePath = Path.Combine(pathToContent, "index.md").ToString();
            MapMetaData(markdownMetaData, Website.Index);

            // Collecting pages and sections data
            string[] directoriesOfContent = Directory.GetDirectories(pathToContent);
            foreach (string directory in directoriesOfContent)
            {
                string nameOfCurrentDirectory = Path.GetFileName(directory);
                if (!Website.MakeSectionsFor.Contains(nameOfCurrentDirectory))
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
                            currentPage.Content = Markdown.ToHtml(markdown: MarkdownFactory.ParseContent(Path.Combine(pathToContent, directory, filename)), pipeline: pipeline);

                            currentPage.MarkdownFileName = filename;
                            currentPage.MarkdownFilePath = Path.Combine(directory, filename).ToString();
                            currentPage.Hierarchy = nameOfCurrentDirectory;
                            Website.Pages.Add(currentPage);
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
                                currentSection.Content = Markdown.ToHtml(markdown: MarkdownFactory.ParseContent(Path.Combine(pathToContent, directory, filename)), pipeline: pipeline);

                                currentSection.MarkdownFileName = filename;
                                currentSection.MarkdownFilePath = Path.Combine(directory, filename).ToString();
                            }
                            else
                            {
                                // Add item
                                Item currentItem = new Item();
                                Dictionary<string, string> currentMetaData = MarkdownFactory.ParseMetaData(Path.Combine(pathToContent, directory, filename));
                                MapMetaData(MarkdownFactory.ParseMetaData(Path.Combine(pathToContent, directory, filename)), currentItem);
                                currentItem.Content = Markdown.ToHtml(markdown: MarkdownFactory.ParseContent(Path.Combine(pathToContent, directory, filename)), pipeline: pipeline);

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

                        Website.Sections.Add(currentSection);
                    }
                }
            }
        }
    }
}
