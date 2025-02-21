using Application.Common.Enums;
using Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Common.Services;

internal class SearchEngineScrapeStrategyFactory(IServiceProvider serviceProvider) : ISearchEngineScrapeStrategyFactory
{
    public ISearchEngineScraper GetSearchEngineScrapeStrategy(SearchEngine engine)
    {
        return engine switch
        {
            SearchEngine.Google => serviceProvider.GetKeyedService<ISearchEngineScraper>("Google")!,
            SearchEngine.Bing => serviceProvider.GetKeyedService<ISearchEngineScraper>("Bing")!,
            _ => throw new NotImplementedException()
        };
    }
}