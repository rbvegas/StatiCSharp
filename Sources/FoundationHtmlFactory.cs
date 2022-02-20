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
                              .Add(new Div()
                                    .Add(new Div(website.Index.Content)
                                            .Class("welcomeWrapper"))
                                    .Add(new Text("<h2>Latest Content</h2>"))
                                    .Add(new ItemList(website))
                                    .Class("wrapper"))
                               
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
                        .Add( new SocialIcons())
                        .Render();
            }
        }

        private class SocialIcons: IHtmlComponent
        {
            public string Render(){
                return new Div()
                .Add(new A("<img src=\"/socialIcons/mail.svg\">").Href("mailto:hi@rolandbraun.com"))
                .Add(new A("<img src=\"/socialIcons/linkedin.svg\">").Href("https://linkedin.com/in/rolandbraun-dev"))
                .Add(new A("<img src=\"/socialIcons/github.svg\">").Href("https://github.com/rolandbraun-dev"))
                .Class("social-icons")
                .Render();
            }
        }

        private class ItemList: IHtmlComponent
        {
            private IWebsite website;
            private List<IItem> items;
            private List<IHtmlComponent> components = new List<IHtmlComponent>();
            public ItemList(IWebsite website)
            {
                this.website = website;
                this.items = new List<IItem>();
                foreach (ISection section in website.Sections)
                {
                    section.Items.ForEach((item) => this.items.Add(item));
                }
            }
                      
            public string Render()
            {
                var result = new Ul().Class("item-list");
                items.ForEach((item) => result.Add(
                                                new Li()
                                                    .Add(new Article()
                                                        .Add(new Text($"<h1>{item.Title}</h1>"))
                                                        .Add(new Text($"<p>{item.Description}</p>"))
                                                    )
                                                )
                                        );
                return result.Render();
            }
        }
    }
}
