using Application.Common.Interfaces;
using Infrastructure.Scraper;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace Infrastructure.Common.Services;

public abstract class SearchEngineScrapeStrategy : ISearchEngineScraper
{
    protected readonly int _maxResults;
    private readonly string _seleniumGridUrl;

    protected SearchEngineScrapeStrategy(IOptions<ScraperSettings> options)
    {
        _seleniumGridUrl = options.Value.SeleniumGridUrl!;
        _maxResults = options.Value.MaxResults;
    }

    protected abstract string AnchorSelector { get; }
    protected abstract string ResultsSelector { get; }

    public List<int> GetSearchRankings(string keyword, string targetUrl, CancellationToken cancellationToken)
    {
        List<int> rankings = [];
        var options = new ChromeOptions();
        options.AddExcludedArgument("enable-automation");
        options.AddArgument("--disable-blink-features=AutomationControlled");

        using (var driver = new RemoteWebDriver(new Uri(_seleniumGridUrl), options))
        {
            driver.Navigate().GoToUrl(Url(keyword));

            var results = driver.FindElements(By.CssSelector(ResultsSelector));
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

            driver.Quit();
        }

        return rankings;
    }

    protected abstract string Url(string keyword);
}