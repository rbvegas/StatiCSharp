using StatiCSharp.Interfaces;
using StatiCSharp.Exceptions;

namespace StatiCSharp;

public partial class WebsiteManager : IWebsiteManager
{
    /// <summary>
    /// Checks if all nessessary directories exist and if it can read and write in this folders.<br/>
    /// If a directory does not exist, it tries to create it.
    /// </summary>
    /// <exception cref="CannotCreateDirectoryException"></exception>
    /// <exception cref="DirectoryNotWriteableException"></exception>
    private void CheckEnvironment(string? templateResources = null)
    {
        string[] assumedDirectories = new string[] {Output, Content, Resources};
        foreach (string assumedDirectory in assumedDirectories)
        {
            if (!Directory.Exists(assumedDirectory))
            {
                try
                {
                    Directory.CreateDirectory(assumedDirectory);
                }
                catch (Exception ex)
                {
                    throw new CannotCreateDirectoryException(message: $"Your {nameof(assumedDirectory).ToLower()} directory does not exist. Trying to create it failed. Do you have read and write access to {assumedDirectory} ?", ex);
                }
            }

            try
            {
                DirectoryIsWritable(assumedDirectory);
            }
            catch (Exception ex)
            {
                throw new DirectoryNotWriteableException(message: $"Trying to write to {assumedDirectory} failed. Do you have read and write access?", ex);
            }
        }


        if (templateResources is not null)
        {
            if (!Directory.Exists(templateResources!))
            {
                throw new DirectoryNotFoundException($"Your template resources directory does not exist. Do you have read and write access to {templateResources} ?");
            }
        }


        void DirectoryIsWritable(string path)
        {
            using (FileStream fs = File.Create(Path.Combine(path, Path.GetRandomFileName()), 1, FileOptions.DeleteOnClose))
            {
            }

        }
    }
}
