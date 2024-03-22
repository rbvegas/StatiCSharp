using System.IO;
using System.Threading.Tasks;

namespace StatiCSharp.Interfaces;

internal interface IFileSystemService
{
    /// <summary>
    /// Deletes all directories and files within the given path, without deleting the path folder itself.
    /// </summary>
    /// <param name="path"></param>
    /// <returns>void</returns>
    Task DeleteAllAsync(string path);

    /// <summary>
    /// Asynchronously copies all directories and files (incl. subfolders and -files) from the source directory to the destination directory.
    /// </summary>
    /// <param name="sourceDir">Source directory</param>
    /// <param name="destinationDir">Source directory</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous copying operation.</returns>
    /// <exception cref="DirectoryNotFoundException"></exception>
    Task CopyAllAsync(string sourceDir, string destinationDir);

    /// <summary>
    /// Writes a file to disc asynchronously.
    /// </summary>
    /// <param name="path">The traget directory path.</param>
    /// <param name="filename">The filename for the file.</param>
    /// <param name="content">The content of the file.</param>
    /// <param name="gitMode">Optional. If true, an existing file is only rewritten if the content changes.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    Task WriteFileAsync(string path, string filename, string content, bool gitMode = false);

    /// <summary>
    /// Writes a file to disc.
    /// </summary>
    /// <param name="path">The traget directory-path.</param>
    /// <param name="filename">The filename for the file.</param>
    /// <param name="content">The content the file will have.</param>
    /// <param name="gitMode">Optional. If true, an existing file is only rewritten if the content changes.</param>
    void WriteFile(string path, string filename, string content, bool gitMode = false);
}