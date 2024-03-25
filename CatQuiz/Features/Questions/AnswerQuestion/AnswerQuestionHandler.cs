using CatQuiz.Core.Exceptions;
using CatQuiz.Data;
using CatQuiz.Entities;
using CatQuiz.Shared.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CatQuiz.Features.Questions.AnswerQuestion;

internal sealed class AnswerQuestionHandler : IRequestHandler<AnswerQuestionRequest, AnswerQuestionResponse>
{
    private readonly ILogger<AnswerQuestionHandler> _logger;
    private readonly DataContext _context;

    public AnswerQuestionHandler(ILogger<AnswerQuestionHandler> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<AnswerQuestionResponse> Handle(AnswerQuestionRequest request, CancellationToken cancellationToken)
    {
        var question = await _context.Questions
            .AsQueryable()
            .FirstOrDefaultAsync(q => q.Id == request.QuestionId && q.UserId == request.UserId);

        if (question is null)
        {
            throw new NotFoundException(nameof(Question), nameof(Question.Id), request.QuestionId.ToString());
        }

        if (question.AnswerStatus != AnswerStatus.Unanswered)
        {
            _logger.LogError($"User with Id: {request.UserId} attempted to answer an already answered question");
            throw new BadRequestException("This question has already been answered");
        }

        var isCorrectAnswer = request.BreedId == question.CorrectBreedId;
        question.AnswerStatus = isCorrectAnswer ? AnswerStatus.Correct : AnswerStatus.Incorrect;
        await _context.SaveChangesAsync();

        return new AnswerQuestionResponse
        {
            IsCorrect = isCorrectAnswer
        };
    }
}
