using Infrastructure.Scraper;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;

namespace Infrastructure.Common.Services;

public class BingSearchEngineScrapeStrategy(IOptions<ScraperSettings> options, Lazy<IWebDriver> webDriver)
    : SearchEngineScrapeStrategy(options, webDriver)
{
    public override string Name => "Bing";

    protected override string AnchorSelector => "a";
    protected override string ResultsSelector => "li.b_algo";

    protected override string Url(string query)
        => string.Format("https://www.bing.com/search?q={0}&count={1}", Uri.EscapeDataString(query), _maxResults);
}