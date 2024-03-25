using CatQuiz.Shared.Breeds;

namespace CatQuiz.Features.Questions.GenerateQuestion;

public class ExternalCatImageDto
{
    public required string Id { get; set; }

    public required string Url { get; set; }

    public List<ExternalBreedDto> Breeds { get; set; }
}
