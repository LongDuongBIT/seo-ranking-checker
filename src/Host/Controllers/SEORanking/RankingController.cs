using Application.Common.Enums;
using Application.SEORanking.Retrieve;
using Host.Controllers.SEORanking.Requests;
using Host.Controllers.SEORanking.Responses;
using Microsoft.AspNetCore.Mvc;
using Shared.Helpers;

namespace Host.Controllers.SEORanking
{
    public class RankingController : VersionedApiController
    {
        [HttpPost]
        public async Task<List<FrontEndRetrieveSEORankingResult>> Retrieve(FrontEndRetrieveSEORankingRequest request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new RetrieveSEORankingRequest
            {
                Keyword = request.Keyword,
                Url = request.Url,
                SearchEngines = request.SearchEngines.Select(se => se.ToEnum<SearchEngine>()).ToArray()
            }, cancellationToken);

            return result.Select(r => new FrontEndRetrieveSEORankingResult
            {
                SearchEngine = r.SearchEngine,
                Rankings = r.Rankings
            }).ToList();
        }
    }
}