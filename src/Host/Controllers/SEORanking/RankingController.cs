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
        public async Task<ActionResult<FrontEndRetrieveSEORankingResponse>> Retrieve(FrontEndRetrieveSEORankingRequest request, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new RetrieveSEORankingRequest
            {
                Keyword = request.Keyword,
                Url = request.Url,
                SearchEngine = request.SearchEngine.ToEnum<SearchEngine>()
            }, cancellationToken));
        }
    }
}