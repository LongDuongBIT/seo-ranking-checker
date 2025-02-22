using Application.Common.Enums;

namespace Application.Common.Interfaces;

public interface ISearchEngineScrapeStrategyFactory : IScopedService
{
    ISearchEngineScraper[] GetSearchEngineScrapeStrategies(SearchEngine[] engine);
}