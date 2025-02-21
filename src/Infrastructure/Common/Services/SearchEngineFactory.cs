using Application.Common.Enums;
using Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Common.Services;

internal class SearchEngineFactory(IServiceProvider serviceProvider) : ISearchEngineFactory
{
    public ISearchEngine GetSearchEngine(SearchEngine engine)
    {
        return engine switch
        {
            SearchEngine.Google => serviceProvider.GetKeyedService<ISearchEngine>("Google")!,
            SearchEngine.Bing => serviceProvider.GetKeyedService<ISearchEngine>("Bing")!,
            _ => throw new NotImplementedException()
        };
    }
}