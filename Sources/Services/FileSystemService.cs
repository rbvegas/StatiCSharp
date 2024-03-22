using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using StatiCSharp.Interfaces;

namespace StatiCSharp.Services;

class FileSystemService : IFileSystemService
{
    /// <inheritdoc/>
    public Task DeleteAllAsync(string path)
    {
        var deletionTask = new Task( () => 
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
        });

        return Task.Run(() => deletionTask);
    }

    /// <inheritdoc/>
    public Task CopyAllAsync(string sourceDir, string destinationDir)
    {
        // https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-copy-directories

        var copyingTask = new Task( () =>
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath, true);
            }

            // If recursive and copying subdirectories, recursively call this method
            if (true)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    var recursiveTask = this.CopyAllAsync(subDir.FullName, newDestinationDir);
                    recursiveTask.Wait();
                }
            }
        });

        return Task.Run(() => copyingTask);
    }

    /// <inheritdoc/>
    public async Task WriteFileAsync(string path, string filename, string content, bool gitMode = false)
    {
        string filePath = Path.Combine(path, filename);

        if (gitMode && File.Exists(filePath))
        {
            string oldFile = await File.ReadAllTextAsync(filePath);
            
            if (oldFile == content)
                return;
        }
        
        await File.WriteAllTextAsync(filePath, content);
    }

    /// <inheritdoc/>
    public void WriteFile(string path, string filename, string content, bool gitMode = false)
    {
        string filePath = Path.Combine(path, filename);
        if (gitMode)
        {
            if (File.Exists(filePath))
            {
                string oldFile = File.ReadAllText(filePath);
                if (oldFile != content)
                {
                    File.WriteAllText(filePath, content);
                }
            }
            else
            {
                File.WriteAllText(filePath, content);
            }
        }
        else
        {
            File.WriteAllText(filePath, content);
        }
    }
}