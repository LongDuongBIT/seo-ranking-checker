using Infrastructure.Common.Services;
using Infrastructure.Scraper;
using Microsoft.Extensions.Options;
using NSubstitute;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace Infrastructure.Tests.Services;

public class BingSearchEngineScrapeStrategyTest
{
    private IOptions<ScraperSettings> _settings;

    [Test]
    public async Task BingSearchEngineScrapeStrategy_GetSearchEngineScrapeStrategies_ShouldReturnRankingsSuccessfully()
    {
        // Arrange
        var webDriver = Substitute.For<IWebDriver>();
        var searchResultsElement = Substitute.For<IWebElement>();
        searchResultsElement.Text.Returns("text");
        searchResultsElement.GetAttribute("href").Returns("href");

        var itemElement = Substitute.For<IWebElement>();
        itemElement.GetAttribute("href").Returns("targetUrl");
        searchResultsElement.FindElement(Arg.Any<By>()).Returns(itemElement);

        webDriver.FindElements(Arg.Any<By>()).Returns(new ReadOnlyCollection<IWebElement>([searchResultsElement]));

        var searchEngineScrapeStrategy = new BingSearchEngineScrapeStrategy(_settings, webDriver);

        // Act
        var result = await searchEngineScrapeStrategy.GetSearchRankings("keyword", "targetUrl", default);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Has.Count.EqualTo(1));
        });
    }

    [Test]
    public async Task BingSearchEngineScrapeStrategy_GetSearchEngineScrapeStrategies_ShouldReturnEmptyRankings()
    {
        // Arrange
        var webDriver = Substitute.For<IWebDriver>();
        var searchResultsElement = Substitute.For<IWebElement>();
        searchResultsElement.Text.Returns("text");
        searchResultsElement.GetAttribute("href").Returns("href");

        var itemElement = Substitute.For<IWebElement>();
        itemElement.GetAttribute("href").Returns("targetUrl");
        searchResultsElement.FindElement(Arg.Any<By>()).Returns(itemElement);

        webDriver.FindElements(Arg.Any<By>()).Returns(new ReadOnlyCollection<IWebElement>([searchResultsElement]));

        var searchEngineScrapeStrategy = new BingSearchEngineScrapeStrategy(_settings, webDriver);

        // Act
        var result = await searchEngineScrapeStrategy.GetSearchRankings("keyword", "anotherUrl", default);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        });
    }

    [SetUp]
    public void SetUp()
    {
        _settings = Options.Create(new ScraperSettings
        {
            MaxResults = 100,
            SeleniumGridUrl = "http://localhost:4444/wd/hub"
        });
    }
}