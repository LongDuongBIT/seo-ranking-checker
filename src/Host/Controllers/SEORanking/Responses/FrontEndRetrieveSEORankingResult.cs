namespace Host.Controllers.SEORanking.Responses;

public class FrontEndRetrieveSEORankingResult
{
    public required List<int> Rankings { get; set; }
    public required string SearchEngine { get; set; }
}