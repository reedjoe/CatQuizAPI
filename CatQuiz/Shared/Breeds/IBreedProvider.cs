namespace CatQuiz.Shared.Breeds;

public interface IBreedProvider
{
    List<ExternalBreedDto> Breeds { get; }

    public Task LoadBreeds();
}
