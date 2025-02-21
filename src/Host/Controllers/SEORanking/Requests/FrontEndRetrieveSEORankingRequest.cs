namespace Host.Controllers.SEORanking.Requests;

public class FrontEndRetrieveSEORankingRequest
{
    public required string Keyword { get; set; }
    public required string Url { get; set; }
    public required string SearchEngine { get; set; }
}