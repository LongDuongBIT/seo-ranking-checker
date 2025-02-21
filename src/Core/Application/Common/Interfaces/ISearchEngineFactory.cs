using Application.Common.Enums;

namespace Application.Common.Interfaces;

public interface ISearchEngineFactory : IScopedService
{
    ISearchEngine GetSearchEngine(SearchEngine engine);
}