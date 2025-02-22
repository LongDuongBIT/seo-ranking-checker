using Application.Common.Caching;
using Application.Common.Interfaces;
using MediatR;

namespace Application.SEORanking.Retrieve;

public class RetrieveSEORankingRequestHandler(
    ISearchEngineScrapeStrategyFactory searchEngineFactory,
    ICacheService cacheService) : IRequestHandler<RetrieveSEORankingRequest, List<SEORankingResult>>
{
    public async Task<List<SEORankingResult>> Handle(RetrieveSEORankingRequest request, CancellationToken cancellationToken)
    {
        var searchEngines = searchEngineFactory.GetSearchEngineScrapeStrategies(request.SearchEngines);
        var tasks = searchEngines.Select(GetCachedSearchRanking(cacheService, request, cancellationToken));
        var results = await Task.WhenAll(tasks);
        return [.. results];
    }

    private static Func<ISearchEngineScraper, Task<SEORankingResult>> GetCachedSearchRanking(ICacheService cacheService, RetrieveSEORankingRequest request, CancellationToken cancellationToken)
    {
        return async engine =>
        {
            string cacheKey = $"seo_rankings_{request.Keyword}_{request.Url}_{engine.GetType().Name}";
            var cachedResult = await cacheService.GetAsync<SEORankingResult>(cacheKey, cancellationToken);

            if (cachedResult != null)
            {
                return cachedResult;
            }

            var result = await engine.GetSearchRankings(request.Keyword, request.Url, cancellationToken);
            var ranking = new SEORankingResult
            {
                SearchEngine = engine.Name,
                Rankings = result
            };

            await cacheService.SetAsync(cacheKey, ranking, TimeSpan.FromHours(1), cancellationToken);

            return ranking;
        };
    }
}