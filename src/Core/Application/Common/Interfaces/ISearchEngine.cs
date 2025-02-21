namespace Application.Common.Interfaces;

public interface ISearchEngine
{
    List<int> GetSearchRankings(string keyword, string targetUrl, CancellationToken cancellationToken);
}