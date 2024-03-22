using System.Collections.Generic;

namespace StatiCSharp.Interfaces;

internal interface IMetaDataService
{
    void Map(Dictionary<string, string> metaData, ISite site, HtmlBuilder htmlBuilder);
}