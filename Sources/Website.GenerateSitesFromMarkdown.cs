using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatiCsharp.Interfaces;
using Markdig;

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
            website.Index.MarkdownFileName = MarkdownFilePath.Substring(0, MarkdownFilePath.LastIndexOf(".md")).Replace(" ", "-").Replace("_", "-").Trim();
            MapMetaData(mdMetaData, website.Index);

            // Collecting pages and sections data
            string[] directoriesOfContent = Directory.GetDirectories(pathToContent);
            foreach (string directory in directoriesOfContent)
            {
                string nameOfCurrentDirectory = Path.GetFileName(directory);
                if (!website.MakeSectionsFor.Contains(nameOfCurrentDirectory))
                {
                    // Add a page
                    string[] filenames = Directory.GetFiles(directory);
                    for (int i = 0; i < filenames.Length; i++)
                    {
                        filenames[i] = Path.GetFileName(filenames[i]);
                    }

                    if (filenames.Length > 0)
                    {
                        foreach (string filename in filenames)
                        {
                            IPage currentPage = new Page();
                            Dictionary<string, string> currentMetaData = MarkdownFactory.ParseMetaData(Path.Combine(pathToContent, directory, filename));
                            currentPage.Content = Markdown.ToHtml(MarkdownFactory.ParseContent(Path.Combine(pathToContent, directory, filename)));

                            string mdFilePath = Path.GetFileName(Path.Combine(pathToContent, directory, filename)).Replace(" ", "-").Trim();
                            currentPage.MarkdownFileName = mdFilePath.Substring(0, mdFilePath.LastIndexOf(".md")).Replace(" ", "-").Replace("_", "-").Trim();

                            MapMetaData(currentMetaData, currentPage);
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

                    string[] filenames = Directory.GetFiles(directory);
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
                                Dictionary<string, string> currentMetaData = MarkdownFactory.ParseMetaData(Path.Combine(pathToContent, directory, filename));
                                currentSection.Content = Markdown.ToHtml(MarkdownFactory.ParseContent(Path.Combine(pathToContent, directory, filename)));

                                string mdFilePath = Path.GetFileName(Path.Combine(pathToContent, directory, filename));
                                currentSection.MarkdownFileName = mdFilePath.Substring(0, mdFilePath.LastIndexOf(".md")).Replace(" ", "-").Replace("_", "-").Trim();

                                MapMetaData(currentMetaData, currentSection);
                            }
                            else
                            {
                                // Add item
                                Item currentItem = new Item();
                                Dictionary<string, string> currentMetaData = MarkdownFactory.ParseMetaData(Path.Combine(pathToContent, directory, filename));
                                currentItem.Content = Markdown.ToHtml(MarkdownFactory.ParseContent(Path.Combine(pathToContent, directory, filename)));

                                currentItem.DateLastModified = DateOnly.FromDateTime(Directory.GetLastWriteTime(Path.Combine(pathToContent, directory, filename)));
                                if (currentMetaData["date"] == string.Empty)
                                {
                                    currentMetaData["date"] = currentItem.DateLastModified.ToString();
                                }

                                string mdFilePath = Path.GetFileName(Path.Combine(pathToContent, directory, filename));
                                currentItem.MarkdownFileName = mdFilePath.Substring(0, mdFilePath.LastIndexOf(".md")).Replace(" ", "-").Replace("_", "-").Trim();

                                MapMetaData(currentMetaData, currentItem);

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
