using System;
using System.IO;
using StatiCSharp.Exceptions;
using StatiCSharp.Interfaces;

namespace StatiCSharp.Services;

/// <summary>
/// Provides useful methods to work with the given environment during the website generating process.
/// </summary>
public class EnvironmentService : IEnvironmentService
{
    /// <summary>
    /// Checks if all nessessary directories exist and if it can read and write in this folders.<br/>
    /// If a directory does not exist, it tries to create it.
    /// </summary>
    /// <param name="outputPath"></param>
    /// <param name="contentPath"></param>
    /// <param name="resourcesPath"></param>
    /// <param name="templateResources"></param>
    /// <returns></returns>
    /// <exception cref="DirectoryNotFoundException"></exception>
    /// <exception cref="CannotCreateDirectoryException"></exception>
    public void CheckEnvironment(string outputPath, string contentPath, string resourcesPath, string? templateResources = null)
    {
        string[] assumedDirectories = new string[] { outputPath, contentPath, resourcesPath };

        foreach(string assumedDirectory in assumedDirectories)
        {
            CheckIfDirectoryExists(assumedDirectory);
            CheckIfDirectoryIsWritable(assumedDirectory);
        }

        if (templateResources is not null)
        {
            if (!Directory.Exists(templateResources!))
            {
                throw new DirectoryNotFoundException($"Your template resources directory does not exist. Do you have read and write access to {templateResources} ?");
            }
        }


        void CheckIfDirectoryExists(string assumedDirectory)
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


        void CheckIfDirectoryIsWritable(string path)
        {
            using (FileStream fs = File.Create(Path.Combine(path, Path.GetRandomFileName()), 1, FileOptions.DeleteOnClose))
            {
            }
        }
    }
}