using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatiCsharp.Interfaces;

namespace StatiCsharp
{
    public partial class Website: IWebsite
    {
        /// <summary>
        /// Copies all directories and files (incl. subfolders and -files) from the source-directory to the destination-directory.
        /// </summary>
        /// <param name="sourceDir">Source directory</param>
        /// <param name="destinationDir">Destination directory</param>
        /// <exception cref="DirectoryNotFoundException"></exception>
        private void CopyAll(string sourceDir, string destinationDir)
        {
            // https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-copy-directories


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
                    CopyAll(subDir.FullName, newDestinationDir);
                }
            }
        }
    }
}
