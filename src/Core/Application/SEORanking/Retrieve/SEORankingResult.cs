namespace Application.SEORanking.Retrieve;

public class SEORankingResult
{
    public required List<int> Rankings { get; set; }
    public required string SearchEngine { get; set; }
}