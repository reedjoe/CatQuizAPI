using CatQuiz.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatQuiz.Features.Questions.AnswerQuestion;

public class AnswerQuestionController : ApiControllerBase
{
    [HttpPost("/api/question/answer")]
    public async Task<AnswerQuestionResponse> List([FromBody] AnswerQuestionRequest request)
    {
        return await Mediator.Send(request);
    }
}

public class AnswerQuestionRequest : IRequest<AnswerQuestionResponse>
{
    public Guid UserId { get; set; }

    public Guid QuestionId { get; set; }

    public required string BreedId { get; set; }
}

public class AnswerQuestionResponse
{
    public bool IsCorrect { get; set; }
}
