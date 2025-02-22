namespace Host.Controllers.SEORanking.Responses;

public class FrontEndRetrieveSEORankingResponse
{
    public required SEORankingResponse[] Results { get; set; }

    public class SEORankingResponse
    {
        public required int[] Rankings { get; set; }
        public required string SearchEngine { get; set; }
    }
}