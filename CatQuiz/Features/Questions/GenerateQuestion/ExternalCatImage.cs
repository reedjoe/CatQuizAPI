using CatQuiz.Shared.Breeds;

namespace CatQuiz.Features.Questions.GenerateQuestion;

public class ExternalCatImage
{
    public required string Id { get; set; }

    public required string Url { get; set; }

    public List<ExternalBreedDto> Breeds { get; set; }
}
