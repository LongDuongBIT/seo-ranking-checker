using Application.Common.Enums;
using MediatR;

namespace Application.SEORanking.Retrieve;

public class RetrieveSEORankingRequest : IRequest<List<SEORankingResult>>
{
    public required string Keyword { get; set; }
    public required string Url { get; set; }
    public required SearchEngine[] SearchEngines { get; set; }
}