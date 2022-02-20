using System;
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
        public string ResourcePaths
        {
            get { return Path.Combine(website.SourceDir, "styles.css"); }
        }

        private IWebsite? website;
        public IWebsite? Website
        {
            get { return this.Website; }
        }

        public void WithWebsite(IWebsite website)
        {
            this.website = website;
        }

        public string MakeIndexHtml(IWebsite website)
        {
            return  new HTML().Add(new SiteHeader(website))
                              .Add(new Div("Welcome to the body").Class("wrapper"))
                    .Render();
        }

        public string MakePageHtml(IPage page)
        {
            return new HTML().Add(new SiteHeader(website)).Render();
        }

        public string MakeSectionHtml(ISection section)
        {
            return new HTML().Add(new SiteHeader(website)).Render();
        }

        public string MakeItemHtml(IItem item)
        {
            return new HTML().Add(new Text().Add("This is an ITEM")).Render();
        }



        ////////////
        /// Components
        ////////////
        
        private class SiteHeader : IHtmlComponent
        {
            List<string> sections;
            IWebsite website;
            public SiteHeader(IWebsite website)
            {
                this.website=website;
                this.sections = website.MakeSectionsFor;
            }
            public string Render()
            {
                Ul NavLinks = new();
                foreach (var section in sections)
                {
                    if (section.ToString() is not null)
                    {
                        NavLinks.Add(new Li(new A(section).Href($"/{section}")));
                    }
                }
                return new Header(
                                new Div(
                                    new A(this.website.Name).Href("/").Class("site-name")
                                ).Add(
                                    new Nav().Add(
                                        new Ul().Add(NavLinks)
                                    )
                                ).Class("wrapper")
                        )
                        .Render();
            }
        }
    }
}
