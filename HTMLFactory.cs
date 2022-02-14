using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using StatiCsharp.HtmlComponents;

namespace StatiCsharp
{   
    /// <summary>
    /// Creates the html and folder structure based on the given website
    /// </summary>
    public static class HtmlFactory
    {
        internal static string MakeIndexHtml()
        {
            return new HTML().Add(
                            new Div().Add(
                                new Text().Add(
                                    "Here is some text in the div. <h1>with a class</h1>"
                                )
                            ).Class("myHornyClass").Style("color:red;")
            ).Render();
        }
    }
}
