using CatQuiz.Core.Exceptions;
using CatQuiz.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CatQuiz.Shared.Enums;

namespace CatQuiz.Features.Rankings.ListUserRankings;

internal sealed class ListUserRankingsHandler : IRequestHandler<ListUserRankingsRequest, ListUserRankingsResponse>
{
    private readonly DataContext _context;

    public ListUserRankingsHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<ListUserRankingsResponse> Handle(ListUserRankingsRequest request, CancellationToken cancellationToken)
    {
        var usersWithQuestions = await _context.Users
            .AsQueryable()
            .AsNoTracking()
            .Include(u => u.Questions)
            .ToListAsync();

        if (usersWithQuestions.Any() != true)
        {
            throw new NotFoundException();
        }

        var userRankings = usersWithQuestions.Select(u => new UserRankingDto
        {
            UserId = u.Id,
            FullName = $"{u.FirstName} {u.LastName}",
            AnswerCount = u.Questions.Count,
            CorrectAnswerCount = u.Questions.Where(q => q.AnswerStatus == AnswerStatus.Correct).Count()
        })
        .OrderByDescending(ur => ur.AnswerCount == 0 ? 0 : (double)ur.CorrectAnswerCount / ur.AnswerCount);

        return new ListUserRankingsResponse
        {
            Count = usersWithQuestions.Count,
            UserRankings = userRankings.Select((userRanking, i) =>
            {
                userRanking.Rank = i + 1;
                return userRanking;
            })
            .ToList()
        };
    }
}