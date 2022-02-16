﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using StatiCsharp.HtmlComponents;
using StatiCsharp.Interfaces;

namespace StatiCsharp
{   
    /// <summary>
    /// 
    /// </summary>
    public class FoundationHtmlFactory: IHtmlFactory
    {
        public string MakeIndexHtml(IWebsite website)
        {
            return new HTML().Add(
                    new Text().Add("<h1>" + website.Index.Title + "</h1>")
                   )
                   .Add(new Div().Add(new Text().Add(website.Index.Content)))
                   .Render();
        }

        public string MakePageHtml(IPage page)
        {
            return new HTML().Add(new Text().Add("This is a page")).Render();
        }
    }
}
