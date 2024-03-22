using System;
using System.IO;
using System.Threading.Tasks;
using StatiCSharp.Interfaces;

namespace StatiCSharp.Services;

internal class SiteGenerationService : ISiteGenerationService
{
    public async Task GenerateSitesFromMarkdown(StaticWebApplication staticWebApplication, HtmlBuilder htmlBuilder, IMetaDataService metaDataService)
    {
        string[] directoriesOfContent = Directory.GetDirectories(staticWebApplication.Content);

        // Index
        string pathOfIndex = Path.Combine(staticWebApplication.Content, "index.md");
        if (File.Exists(pathOfIndex))
            LoadSiteFromMarkdown<IIndex>(pathOfIndex, htmlBuilder, staticWebApplication, metaDataService);


        // Pages
        foreach (string directory in directoriesOfContent)
        {
            string nameOfCurrentDirectory = Path.GetFileName(directory);
            if (!staticWebApplication.MakeSectionsFor.Contains(nameOfCurrentDirectory))
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
                    LoadSiteFromMarkdown<IPage>(file, htmlBuilder, staticWebApplication, metaDataService);
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
            if (staticWebApplication.MakeSectionsFor.Contains(nameOfCurrentDirectory))
            {
                string pathOfSectionIndexFile = Path.Combine(directory, "index.md");

                if (File.Exists(pathOfSectionIndexFile))
                    LoadSiteFromMarkdown<ISection>(pathOfSectionIndexFile, htmlBuilder, staticWebApplication, metaDataService);
            }
        }
    }

    private void LoadSiteFromMarkdown<T>(string path, HtmlBuilder htmlBuilder, StaticWebApplication staticWebApplication, IMetaDataService metaDataService)
    {
        var metaData = MarkdownFactory.ParseMetaData(path);
        var content = MarkdownFactory.ParseContent(path);
        var contentAsHtml = htmlBuilder.ToHtml(content);
        var filename = Path.GetFileName(path);

        if (typeof(T) == typeof(IIndex))
        {
            staticWebApplication.Index.Content = contentAsHtml;
            staticWebApplication.Index.MarkdownFileName = filename;
            staticWebApplication.Index.MarkdownFilePath = path;
            metaDataService.Map(metaData, staticWebApplication.Index, htmlBuilder);
            return;
        }

        if (typeof(T) == typeof(IPage))
        {
            IPage currentPage = new Page();
            currentPage.Content = contentAsHtml;
            currentPage.MarkdownFileName = filename;
            currentPage.MarkdownFilePath = path;
            metaDataService.Map(metaData, currentPage, htmlBuilder);

            string hierarchy = path.Remove(0, staticWebApplication.Content.Length);
            if (hierarchy[0] == '/' || hierarchy[0] == '\\')
            {
                hierarchy = hierarchy.Remove(0, 1);
            }

            hierarchy = Path.GetDirectoryName(hierarchy)!;

            currentPage.Hierarchy = hierarchy;

            staticWebApplication.Pages.Add(currentPage);
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
            metaDataService.Map(metaData, currentSection, htmlBuilder);

            string sectionFolder = Path.GetDirectoryName(path)!;
            string[] itemFiles = Directory.GetFiles(sectionFolder);

            foreach (string itemFile in itemFiles)
            {
                if (!itemFile.EndsWith("index.md"))
                {
                    IItem currentItem = new Item();
                    var itemMetaData = MarkdownFactory.ParseMetaData(itemFile);
                    var itemContent = MarkdownFactory.ParseContent(itemFile);
                    var itemContentAsHtml = htmlBuilder.ToHtml(itemContent);
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
                    metaDataService.Map(itemMetaData, currentItem, htmlBuilder);

                    currentSection.AddItem(currentItem);
                }
            }
            staticWebApplication.Sections.Add(currentSection);
            return;
        }

        throw new NotImplementedException(message:$"The given type-parameter {typeof(T)} is not supported by this method.");

    }

}