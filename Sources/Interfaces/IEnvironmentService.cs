namespace StatiCSharp.Interfaces;

internal interface IEnvironmentService
{
    void CheckEnvironment(string outputPath, string contentPath, string resourcesPath, string? templateResources = null);
}