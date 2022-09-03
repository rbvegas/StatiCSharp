using StatiCSharp.Interfaces;

namespace StatiCSharp;

public partial class WebsiteManager : IWebsiteManager
{
    /// <summary>
    /// Deletes all directories and files within the given path, without deleting the path folder itself.
    /// </summary>
    /// <param name="path">Path of the directory.</param>
    private void DeleteAll(string path)
    {
        DirectoryInfo directory = new DirectoryInfo(path);

        foreach (FileInfo file in directory.GetFiles())
        {
            file.Delete();
        }

        foreach (DirectoryInfo dir in directory.GetDirectories())
        {
            dir.Delete(true);
        }
    }

    /// <summary>
    /// Asynchronously deletes all directories and files within the given path, without deleting the path folder itself.
    /// </summary>
    /// <param name="path"></param>
    /// <returns>A task that represents the asynchronous deleting operation.</returns>
    private async Task DeleteAllAsync(string path)
    {
        DirectoryInfo directory = new DirectoryInfo(path);

        foreach (FileInfo file in directory.GetFiles())
        {
            file.Delete();
        }

        foreach (DirectoryInfo dir in directory.GetDirectories())
        {
            dir.Delete(true);
        }
    }
}
