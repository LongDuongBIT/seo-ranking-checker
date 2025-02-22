namespace Application.Common.Interfaces;

public interface ISearchEngineScraper
{
    string Name { get; }

    Task<List<int>> GetSearchRankings(string keyword, string targetUrl, CancellationToken cancellationToken);
}