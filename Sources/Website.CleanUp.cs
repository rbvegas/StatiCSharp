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
        private void CleanUp()
        {
            string[] directories = Directory.GetDirectories(this.Output);
            foreach (string directory in directories)
            {
                cleanUpDirectory(directory);
            }
            

            void cleanUpDirectory(string dir)
            {
                // check if there are folders that are not in _pathDirectory
                string[] currentDirectories = Directory.GetDirectories(dir);
                
                foreach (string directory in currentDirectories)
                {
                    if (!_pathDirectory.Contains(directory))
                    {
                        // Delete only files named index.html. Other files can be resources!
                        if (File.Exists(Path.Combine(directory, "index.html")))
                        {
                            File.Delete(Path.Combine(directory, "index.html"));
                        }
                        
                        if (Directory.GetDirectories(directory).Length == 0 && Directory.GetFiles(directory).Length == 0)
                        {
                            Directory.Delete(directory);
                        }
                        else
                        {
                            foreach (string subdir in Directory.GetDirectories(directory))
                            {
                                cleanUpDirectory(subdir);
                            }
                        }
                    }
                }
            }
        }
    }
}
