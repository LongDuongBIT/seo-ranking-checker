using Application.Common.Enums;

namespace Application.Common.Interfaces;

public interface ISearchEngineScrapeStrategyFactory : IScopedService
{
    ISearchEngineScraper GetSearchEngineScrapeStrategy(SearchEngine engine);
}