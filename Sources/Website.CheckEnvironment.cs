using StatiCsharp.Interfaces;
using static System.Console;

namespace StatiCsharp
{
    public partial class Website : IWebsite
    {
        /// <summary>
        /// Checks if all nessessary folders exist and if StatiC# can read and write in this folders.
        /// If a folder does not exists its created.
        /// Failures are printed in the console.
        /// </summary>
        /// <returns>True, if the check is ok.</returns>
        private bool CheckEnvironment(string? templateResources=null)
        {
            if (!Directory.Exists(Output))
            {
                try
                {
                    Directory.CreateDirectory(Output);
                }
                catch
                {
                    WriteLine($"Your output directory does not exist. Trying to create it failed. Do you have read and write access to {Output} ?");
                    return false;
                }
            }

            try
            {
                DirectoryIsWritable(Output);
            }
            catch
            {
                WriteLine($"Trying to write to {Output} failed. Do you have read and write access?");
                return false;
            }


            if (!Directory.Exists(Content))
            {
                try
                {
                    Directory.CreateDirectory(Content);
                }
                catch
                {
                    WriteLine($"Your content directory does not exist. Trying to create it failed. Do you have read and write access to {Content} ?");
                    return false;
                }
            }

            try
            {
                DirectoryIsWritable(Content);
            }
            catch
            {
                WriteLine($"Trying to write to {Content} failed. Do you have read and write access?");
                return false;
            }


            if (!Directory.Exists(Resources))
            {
                try
                {
                    Directory.CreateDirectory(Resources);
                }
                catch
                {
                    WriteLine($"Your resources directory does not exist. Trying to create it failed. Do you have read and write access to {Resources} ?");
                    return false;
                }
            }

            try
            {
                DirectoryIsWritable(Resources);
            }
            catch
            {
                WriteLine($"Trying to write to {Resources} failed. Do you have read and write access?");
                return false;
            }

            if (templateResources is not null)
            {
                if (!Directory.Exists(templateResources!))
                {
                    WriteLine($"Your template resources directory does not exist. Do you have read and write access to {templateResources} ?");
                    return false;
                }
            }
            return true;

            
            void DirectoryIsWritable(string path)
            {
                try
                {
                    using (
                        FileStream fs = File.Create(Path.Combine(path, Path.GetRandomFileName()), 1, FileOptions.DeleteOnClose)
                        )
                    { }
                }
                catch
                {
                    throw;
                }

            }
        }
    }
}
