using System.Threading.Tasks;

namespace StatiCSharp.Interfaces;

internal interface ISiteGenerationService
{
    /// <summary>
    /// Asynchronous generates index, pages, sections and items for the Website from the markdown files in the Content directory.
    /// </summary>
    /// <returns></returns>
    Task GenerateSitesFromMarkdown(StaticWebApplication staticWebApplication, HtmlBuilder htmlBuilder, IMetaDataService metaDataService);
}