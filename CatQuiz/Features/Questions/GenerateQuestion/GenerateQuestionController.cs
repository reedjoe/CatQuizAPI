using CatQuiz.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatQuiz.Features.Questions.GenerateQuestion;

public class GenerateQuestionController : ApiControllerBase
{
    [HttpPost("/api/question/generate")]
    public async Task<GenerateQuestionResponse> List([FromBody] GenerateQuestionRequest request)
    {
        return await Mediator.Send(request);
    }
}

public class GenerateQuestionRequest : IRequest<GenerateQuestionResponse>
{
    public Guid UserId { get; set; }
}

public class GenerateQuestionResponse
{
    public Guid Id { get; set; }

    public required string ImageUrl { get; set; }

    public List<BreedDto> Breeds { get; set; }
}

public class BreedDto
{
    public required string Id { get; set; }

    public required string Name { get; set; }
}
