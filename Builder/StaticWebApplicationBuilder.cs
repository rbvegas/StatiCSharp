using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using StatiCSharp.Interfaces;
using StatiCSharp.Services;

namespace StatiCSharp.Builder;

/// <summary>
/// A builder for static web applications and services.
/// </summary>
public class StaticWebApplicationBuilder
{
    private string _url = string.Empty;
    private string _name = string.Empty;
    private string _description = string.Empty;
    private CultureInfo _language = new CultureInfo("en-US");
    private List<string> _sections = new List<string>();
    private ServiceCollection _serviceCollection;
    public ServiceCollection Services
    {
        get => _serviceCollection;
        set
        {
            this._serviceCollection = value;
        }
    }

    public StaticWebApplicationBuilder()
    {
        InitializeServiceCollection();
    }

    public StaticWebApplication Build()
    {
        if(!MandatoryPropertiesSet())
        {
            throw new ArgumentException("Unable to build static web application, because not all mandatory properties are set!");
        }

        return new StaticWebApplication(
            url: _url,
            name: _name,
            description: _description,
            language: _language,
            sections: _sections,
            serviceProvider: _serviceCollection.BuildServiceProvider()
        );
    }

    /// <summary>
    /// Sets the absolute domain of the website. E.g. "https://mydomain.com"\.
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public StaticWebApplicationBuilder Url(string url)
    {
        _url = url;
        return this;
    }

    /// <summary>
    /// Sets the name of the website.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public StaticWebApplicationBuilder Name(string name)
    {
        _name = name;
        return this;
    }

    /// <summary>
    /// Sets a short description of the website. Is used for metadata in the html sites.
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    public StaticWebApplicationBuilder Description(string description)
    {
        _description = description;
        return this;
    }

    /// <summary>
    /// Sets the language the websites main content is written in.
    /// </summary>
    /// <param name="language"></param>
    /// <returns></returns>
    public StaticWebApplicationBuilder Language(string language)
    {
        _language = new CultureInfo(language);
        return this;
    }

        /// <summary>
    /// Sets the language the websites main content is written in.
    /// </summary>
    /// <param name="language"></param>
    /// <returns></returns>
    public StaticWebApplicationBuilder Language(CultureInfo language)
    {
        _language = language;
        return this;
    }

    /// <summary>
    /// Sets the collection of the websites section names. Folders in the content directory with names matching one item of this list a treated as sections.
    /// </summary>
    /// <returns></returns>
    public StaticWebApplicationBuilder MakeSectionsFor(List<string> sections)
    {
        _sections.AddRange(sections);
        return this;
    }

        /// <summary>
    /// Sets the collection of the websites section names. Folders in the content directory with names matching one item of this list a treated as sections.
    /// </summary>
    /// <returns></returns>
    public StaticWebApplicationBuilder MakeSectionsFor(string sections)
    {
        _sections.AddRange(sections.Replace(" ", string.Empty).Split(',').ToList());
        return this;
    }

    private bool MandatoryPropertiesSet()
    {
        if (string.IsNullOrEmpty(_url)) return false;
        if (string.IsNullOrEmpty(_name)) return false;
        if (string.IsNullOrEmpty(_description)) return false;
        return true;
    }

    private void InitializeServiceCollection()
    {
        _serviceCollection = new ServiceCollection();
        _serviceCollection.AddSingleton<IFileSystemService, FileSystemService>();
        _serviceCollection.AddSingleton<IEnvironmentService, EnvironmentService>();
        _serviceCollection.AddSingleton<ISiteGenerationService, SiteGenerationService>();
        _serviceCollection.AddSingleton<IMetaDataService, MetaDataService>();
        _serviceCollection.AddSingleton<IHtmlService, HtmlService>();
    }
}