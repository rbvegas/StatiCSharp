using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatiCsharp.Interfaces
{
    public interface ISite
    {
        string Title { get; set; }
        string Description { get; set; }
        string Author { get; set; }
        DateOnly Date { get; set; }
        
        // The path of the site within its corresponding hierarchy
        string Path { get; set; }
        List<string> Tags { get; set; }
        string Content { get; set; }
    }
}
