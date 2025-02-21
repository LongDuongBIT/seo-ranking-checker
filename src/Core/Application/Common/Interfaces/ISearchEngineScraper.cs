namespace Application.Common.Interfaces;

public interface ISearchEngineScraper
{
    List<int> GetSearchRankings(string keyword, string targetUrl, CancellationToken cancellationToken);
}