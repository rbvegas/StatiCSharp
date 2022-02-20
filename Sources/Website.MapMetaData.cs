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
        /// <summary>
        /// Adds the given meta data to a site (index, page, section or item). If there is no field for a given entry, it's sciped.
        /// </summary>
        /// <param name="metaData">The meta data.</param>
        /// <param name="site">The site where to add the meta data.</param>
        private void MapMetaData(Dictionary<string, string> metaData, ISite site)
        {
            try { if (metaData["title"] is not null) { site.Title = metaData["title"]; } } catch { }
            try { if (metaData["description"] is not null) { site.Description = metaData["description"]; } } catch { }
            try { if (metaData["author"] is not null) { site.Author = metaData["author"]; } } catch { }
            try { if (metaData["date"] is not null) { site.Date = DateOnly.Parse(metaData["date"]); } } catch { }
            try { if (metaData["path"] is not null) { site.Path = metaData["path"]; } } catch { }
            try { if (metaData["tags"] is not null) { site.Tags = metaData["tags"].Replace(" ", string.Empty).Split(',').ToList(); } } catch { }
        }
    }
}
