using CatQuiz.Core.Exceptions;
using CatQuiz.Core.Extensions;
using CatQuiz.Data;
using CatQuiz.Entities;
using CatQuiz.Shared.Breeds;
using CatQuiz.Shared.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CatQuiz.Features.Questions.GenerateQuestion;

internal sealed class GenerateQuestionHandler : IRequestHandler<GenerateQuestionRequest, GenerateQuestionResponse>
{
    private const int NumberOfAdditionalBreedOptions = 3;
    private readonly ILogger<GenerateQuestionHandler> _logger;
    private readonly DataContext _context;
    private readonly IBreedProvider _breedProvider;
    private readonly IHttpClientFactory _factory;

    public GenerateQuestionHandler(ILogger<GenerateQuestionHandler> logger, DataContext context, IBreedProvider breedProvider, IHttpClientFactory factory)
    {
        _logger = logger;
        _context = context;
        _breedProvider = breedProvider;
        _factory = factory;
    }

    public async Task<GenerateQuestionResponse> Handle(GenerateQuestionRequest request, CancellationToken cancellationToken)
    {
        if (!await _context.Users.AnyAsync(u => u.Id == request.UserId))
        {
            throw new NotFoundException();
        }

        var question = await GenerateQuestion(request);
        var breedOptions = GenerateBreedOptions(question.CorrectBreedId);

        _context.Questions.Add(question);
        await _context.SaveChangesAsync();

        return new GenerateQuestionResponse
        {
            Id = question.Id,
            ImageUrl = question.ImageUrl,
            Breeds = breedOptions
        };
    }

    private async Task<Question> GenerateQuestion(GenerateQuestionRequest request)
    {
        var selectedBreedId = _breedProvider.Breeds.PickRandom().First().Id;
        var client = _factory.CreateClient("CatApi");
        ExternalCatImage image;

        try
        {
            _logger.LogTrace($"Calling Cat API to retrieve an image for Breed: {selectedBreedId}");
            image = (await client.GetFromJsonAsync<List<ExternalCatImage>>($"images/search?breed_ids={selectedBreedId}")).First();
        }
        catch (Exception)
        {
            _logger.LogError($"Failed to retrieve image data from Cat API for Breed: {selectedBreedId}");
            throw;
        }

        if (image is null)
        {
            throw new Exception();
        }

        return new Question
        {
            ExternalImageId = image.Id,
            ImageUrl = image.Url,
            AnswerStatus = AnswerStatus.Unanswered,
            CorrectBreedId = image.Breeds.First().Id,
            UserId = request.UserId
        };
    }

    private List<BreedDto> GenerateBreedOptions(string correctBreedId)
    {
        var breedList = _breedProvider.Breeds;
        var correctBreed = breedList.FirstOrDefault(b => b.Id == correctBreedId);

        if (correctBreed is null)
        {
            _logger.LogError($"Encountered an invalid Breed with Id: {correctBreedId}");
            throw new Exception();
        }

        breedList.Remove(correctBreed);

        if (breedList.Count < NumberOfAdditionalBreedOptions)
        {
            _logger.LogError($"Insufficient Breeds available to form multiple choice options");
            throw new Exception();
        }

        var breedOptions = breedList
            .PickRandom(NumberOfAdditionalBreedOptions)
            .Append(correctBreed)
            .RandomiseOrder();

        return breedOptions.Select(b => new BreedDto
        {
            Id = b.Id,
            Name = b.Name
        }).ToList();
    }
}
