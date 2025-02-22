namespace Application.SEORanking.Retrieve;

public class RetrieveSEORankingResponse
{
    public required SEORankingResponse[] Results { get; set; }

    public class SEORankingResponse
    {
        public required int[] Rankings { get; set; }
        public required string SearchEngine { get; set; }
    }
}