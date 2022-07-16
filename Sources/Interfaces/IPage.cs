namespace StatiCSharp.Interfaces
{
    public interface IPage: ISite
    {
        /// Hierachy of the page. (i.e. "dev" for rolandbraun.com/dev)
        string Hierarchy { get; set; }
    }
}
