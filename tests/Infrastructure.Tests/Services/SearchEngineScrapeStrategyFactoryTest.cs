using Application.Common.Enums;
using Application.Common.Interfaces;
using Infrastructure.Common.Services;
using Infrastructure.Scraper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace Infrastructure.Tests.Services;

public class SearchEngineScrapeStrategyFactoryTest
{
    private IServiceProvider _serviceProvider;

    [Test]
    public void SearchEngineScrapeStrategyFactory_GetSearchEngineScrapeStrategies_ShouldReturnSearchEngineScrapeStrategies()
    {
        // Arrange
        var searchEngineScrapeStrategyFactory = new SearchEngineScrapeStrategyFactory(_serviceProvider);
        var searchEngines = new SearchEngine[] { SearchEngine.Google, SearchEngine.Bing };

        // Act
        var result = searchEngineScrapeStrategyFactory.GetSearchEngineScrapeStrategies(searchEngines);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Length.EqualTo(2));
        });
    }

    [Test]
    public void SearchEngineScrapeStrategyFactory_GetSearchEngineScrapeStrategies_ShouldThrowNotImplementedException()
    {
        // Arrange
        var searchEngineScrapeStrategyFactory = new SearchEngineScrapeStrategyFactory(_serviceProvider);
        var searchEngines = new SearchEngine[] { SearchEngine.Yahoo };
        // Act
        void Act() => searchEngineScrapeStrategyFactory.GetSearchEngineScrapeStrategies(searchEngines);
        // Assert
        Assert.That(Act, Throws.Exception.TypeOf<NotImplementedException>());
    }

    [SetUp]
    public void SetUp()
    {
        _serviceProvider = Substitute.For<IServiceProvider>();

        var services = new ServiceCollection();
        services.AddKeyedScoped<ISearchEngineScraper, GoogleSearchEngineScrapeStrategy>("Google");
        services.AddKeyedScoped<ISearchEngineScraper, BingSearchEngineScrapeStrategy>("Bing");
        services.AddScoped<IWebDriver, RemoteWebDriver>(f =>
        {
            return new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), new ChromeOptions());
        });

        var configurationBuilder = new ConfigurationBuilder();
        var config = configurationBuilder.Build();

        services.Configure<ScraperSettings>(config.GetSection(nameof(ScraperSettings)));

        _serviceProvider = services.BuildServiceProvider();
    }

    [TearDown]
    public void TearDown()
    {
        if (_serviceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }

        _serviceProvider = null!;
    }
}