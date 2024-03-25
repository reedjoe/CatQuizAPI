using CatQuiz.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatQuiz.Features.Rankings.ListUserRankings;

public class ListUserRankingsController : ApiControllerBase
{
    [HttpGet("/api/rankings/list")]
    public async Task<ListUserRankingsResponse> List([FromQuery] ListUserRankingsRequest request)
    {
        return await Mediator.Send(request);
    }
}

public class ListUserRankingsRequest : IRequest<ListUserRankingsResponse>
{
}

public class ListUserRankingsResponse
{
    public int Count { get; set; }

    public List<UserRankingDto> UserRankings { get; set; }
}

public class UserRankingDto
{
    public Guid UserId { get; set; }

    public required string FullName { get; set; }

    public int Rank { get; set; }

    public int AnswerCount { get; set; }

    public int CorrectAnswerCount { get; set; }
}