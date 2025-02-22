using Application.Common.Interfaces;
using Infrastructure.Scraper;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;

namespace Infrastructure.Common.Services;

public abstract class SearchEngineScrapeStrategy : ISearchEngineScraper
{
    protected readonly int _maxResults;
    private readonly IWebDriver _driver;

    private bool _disposed = false;

    protected SearchEngineScrapeStrategy(IOptions<ScraperSettings> options, IWebDriver driver)
    {
        _maxResults = options.Value.MaxResults;
        _driver = driver;
    }

    public abstract string Name { get; }
    protected abstract string AnchorSelector { get; }
    protected abstract string ResultsSelector { get; }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public Task<List<int>> GetSearchRankings(string keyword, string targetUrl, CancellationToken cancellationToken)
    {
        List<int> rankings = [];

        _driver.Navigate().GoToUrl(Url(keyword));

        var results = _driver.FindElements(By.CssSelector(ResultsSelector));
        int rank = 1;

        foreach (var result in results)
        {
            try
            {
                string link = result.FindElement(By.CssSelector(AnchorSelector)).GetAttribute("href");
                if (link.Contains(targetUrl))
                {
                    rankings.Add(rank);
                }
            }
            catch (NoSuchElementException)
            {
                // simply ignore the exception
            }

            rank++;
        }

        return Task.FromResult(rankings);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _driver?.Quit();
            }

            _disposed = true;
        }
    }

    protected abstract string Url(string keyword);
}