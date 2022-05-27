namespace StatiCsharp.Interfaces
{
    public  interface IItem: ISite
    {
        /// <summary>
        /// The section the items is a part of.
        /// </summary>
        public string Section { get; }
    }
}
