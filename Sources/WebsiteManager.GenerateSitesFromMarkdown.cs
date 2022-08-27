using Markdig;
using StatiCSharp.Interfaces;

namespace StatiCSharp;

public partial class WebsiteManager : IWebsiteManager
{
    /// <summary>
    /// Generates index, pages, sections and items for the IWebsite object from the markdown files in the `Content` directory.
    /// </summary>
    private void GenerateSitesFromMarkdown(string pathToContent)
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .Build();

        // For the index: Collecting meta data and content from markdown files, if there are any
        string pathOfIndex = Path.Combine(pathToContent, "index.md");
        if (File.Exists(pathOfIndex))
        {
            Dictionary<string, string> markdownMetaData = MarkdownFactory.ParseMetaData(pathOfIndex);
            string markdownContent = MarkdownFactory.ParseContent(pathOfIndex);
            Website.Index.Content = Markdown.ToHtml(markdown: markdownContent, pipeline: pipeline);
            string markdownFilePath = Path.GetFileName(pathOfIndex);
            Website.Index.MarkdownFileName = markdownFilePath.Substring(0, markdownFilePath.LastIndexOf(".md"));
            Website.Index.MarkdownFilePath = pathOfIndex;
            MapMetaData(markdownMetaData, Website.Index);
        }

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


    /// <summary>
    /// Asynchronously generates index, pages, sections and items for the IWebsite object from the markdown files in the Content directory.
    /// </summary>
    /// <returns></returns>
    private async Task GenerateSitesFromMarkdownAsync()
    {
        string[] directoriesOfContent = Directory.GetDirectories(Content);

        // Index
        string pathOfIndex = Path.Combine(Content, "index.md");
        if (File.Exists(pathOfIndex))
            await LoadSiteFromMarkdown<IIndex>(pathOfIndex);


        // Pages
        foreach (string directory in directoriesOfContent)
        {
            string nameOfCurrentDirectory = Path.GetFileName(directory);
            if (!Website.MakeSectionsFor.Contains(nameOfCurrentDirectory))
            {
                await ProcessAllPagesInDirectory(directory);
            }
        }

        async Task ProcessAllPagesInDirectory(string dir)
        {
            string[] files = Directory.GetFiles(dir);
            var dirs = Directory.GetDirectories(dir);

            foreach (string file in files)
            {
                if (file.EndsWith(".md"))
                    await LoadSiteFromMarkdown<IPage>(file);
            }

            foreach (string directory in dirs)
            {
                await ProcessAllPagesInDirectory(directory);
            }
        }


        // Sections
        foreach (string directory in directoriesOfContent)
        {
            string nameOfCurrentDirectory = Path.GetFileName(directory);
            if (Website.MakeSectionsFor.Contains(nameOfCurrentDirectory))
            {
                string pathOfSectionIndexFile = Path.Combine(directory, "index.md");

                if (File.Exists(pathOfSectionIndexFile))
                    await LoadSiteFromMarkdown<ISection>(pathOfSectionIndexFile);
            }
        }
    }

    private async Task LoadSiteFromMarkdown<T>(string path)
    {
        var pipeline = new MarkdownPipelineBuilder()
           .UseAdvancedExtensions()
           .Build();

        var metaData = MarkdownFactory.ParseMetaData(path);
        var content = MarkdownFactory.ParseContent(path);
        var contentAsHtml = Markdown.ToHtml(content, pipeline);
        var filename = Path.GetFileName(path);

        if (typeof(T) == typeof(IIndex))
        {
            Website.Index.Content = contentAsHtml;
            Website.Index.MarkdownFileName = filename;
            Website.Index.MarkdownFilePath = path;
            MapMetaData(metaData, Website.Index);
            return;
        }

        if (typeof(T) == typeof(IPage))
        {
            IPage currentPage = new Page();
            currentPage.Content = contentAsHtml;
            currentPage.MarkdownFileName = filename;
            currentPage.MarkdownFilePath = path;
            MapMetaData(metaData, currentPage);

            string hierarchy = path.Remove(0, Content.Length);
            if (hierarchy[0] == '/' || hierarchy[0] == '\\')
            {
                hierarchy = hierarchy.Remove(0, 1);
            }

            hierarchy = Path.GetDirectoryName(hierarchy)!;

            currentPage.Hierarchy = hierarchy;

            Website.Pages.Add(currentPage);
            return;
        }

        if (typeof(T) == typeof(ISection))
        {
            ISection currentSection = new Section();
            string currentSectionName = Path.GetDirectoryName(path)!;
            currentSectionName = Path.GetFileName(currentSectionName);
            currentSection.SectionName = currentSectionName;
            currentSection.Content = contentAsHtml;
            currentSection.MarkdownFileName = filename;
            currentSection.MarkdownFilePath = path;
            MapMetaData(metaData, currentSection);

            string sectionFolder = Path.GetDirectoryName(path)!;
            string[] itemFiles = Directory.GetFiles(sectionFolder);

            foreach (string itemFile in itemFiles)
            {
                if (!itemFile.EndsWith("index.md"))
                {
                    IItem currentItem = new Item();
                    var itemMetaData = MarkdownFactory.ParseMetaData(itemFile);
                    var itemContent = MarkdownFactory.ParseContent(itemFile);
                    var itemContentAsHtml = Markdown.ToHtml(itemContent, pipeline);
                    var itemLastModified = DateOnly.FromDateTime(Directory.GetLastWriteTime(itemFile));

                    if (itemMetaData.ContainsKey("date"))
                    {
                        if (itemMetaData["date"] == string.Empty)
                        {
                            itemMetaData["date"] = currentItem.DateLastModified.ToString();
                        }
                    }
                    else
                    {
                        itemMetaData.Add("date", currentItem.DateLastModified.ToString());
                    }

                    currentItem.Content = itemContentAsHtml;
                    currentItem.MarkdownFileName = Path.GetFileName(itemFile);
                    currentItem.MarkdownFilePath = itemFile;
                    currentItem.Section = currentSectionName;
                    currentItem.DateLastModified = itemLastModified;
                    MapMetaData(itemMetaData, currentItem);

                    currentSection.AddItem(currentItem);
                }
            }
            Website.Sections.Add(currentSection);
        }
    }
}
