using Application.Common.Interfaces;
using MediatR;

namespace Application.SEORanking.Retrieve;

public class RetrieveSEORankingRequestHandler(ISearchEngineScrapeStrategyFactory searchEngineFactory) : IRequestHandler<RetrieveSEORankingRequest, RetrieveSEORankingResponse>
{
    public async Task<RetrieveSEORankingResponse> Handle(RetrieveSEORankingRequest request, CancellationToken cancellationToken)
    {
        var searchEngine = searchEngineFactory.GetSearchEngineScrapeStrategy(request.SearchEngine);
        var ranking = searchEngine.GetSearchRankings(request.Keyword, request.Url, cancellationToken);
        return new RetrieveSEORankingResponse
        {
            Rankings = [.. ranking]
        };
    }
}