using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatiCsharp.Interfaces;
using System.IO;

namespace StatiCsharp
{
    public partial class Website: IWebsite
    {
        /// <summary>
        /// Deletes all directories and files within the given path, without deleting the the path folder itself.
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
    }
}
