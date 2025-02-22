using Infrastructure.Scraper;
using Microsoft.Extensions.Options;

namespace Infrastructure.Common.Services;

internal class BingSearchEngineScrapeStrategy(IOptions<ScraperSettings> options) : SearchEngineScrapeStrategy(options)
{
    public override string Name => "Bing";

    protected override string AnchorSelector => "a";
    protected override string ResultsSelector => "li.b_algo";

    protected override string Url(string query)
        => string.Format("https://www.bing.com/search?q={0}&count={1}", Uri.EscapeDataString(query), _maxResults);
}