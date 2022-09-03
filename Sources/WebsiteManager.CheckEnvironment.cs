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

    /// <summary>
    /// Asynchronously checks if all nessessary directories exist and if it can read and write in this folders.<br/>
    /// If a directory does not exist, it tries to create it.
    /// </summary>
    /// <param name="templateResources"></param>
    /// <returns>A task that represents the asynchronous checking operation.</returns>
    /// <exception cref="DirectoryNotFoundException"></exception>
    /// <exception cref="CannotCreateDirectoryException"></exception>
    private async Task CheckEnvironmentAsync(string? templateResources = null)
    {
        string[] assumedDirectories = new string[] { Output, Content, Resources };

        List<Task> tasks = new List<Task>();

        foreach(string assumedDirectory in assumedDirectories)
        {
            tasks.Add(CheckIfDirectoryExists(assumedDirectory));
            tasks.Add(CheckIfDirectoryIsWritable(assumedDirectory));
        }

        await Task.WhenAll(tasks);

        if (templateResources is not null)
        {
            if (!Directory.Exists(templateResources!))
            {
                throw new DirectoryNotFoundException($"Your template resources directory does not exist. Do you have read and write access to {templateResources} ?");
            }
        }


        async Task CheckIfDirectoryExists(string assumedDirectory)
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
        }


        async Task CheckIfDirectoryIsWritable(string path)
        {
            using (FileStream fs = File.Create(Path.Combine(path, Path.GetRandomFileName()), 1, FileOptions.DeleteOnClose))
            {
            }
        }
    }
}
