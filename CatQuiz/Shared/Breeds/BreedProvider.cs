namespace CatQuiz.Shared.Breeds;

public class BreedProvider : IBreedProvider
{
    private readonly ILogger<BreedProvider> _logger;
    private readonly IHttpClientFactory _factory;
    private List<ExternalBreedDto> _breeds = new List<ExternalBreedDto>();

    public List<ExternalBreedDto> Breeds { get { return _breeds.ToList(); } }

    public BreedProvider(ILogger<BreedProvider> logger, IHttpClientFactory factory)
    {
        _logger = logger;
        _factory = factory;
    }

    public async Task LoadBreeds()
    {
        var client = _factory.CreateClient("CatApi");

        try
        {
            _logger.LogTrace("Calling Cat API to retrieve list of all available Breeds");
            _breeds = await client.GetFromJsonAsync<List<ExternalBreedDto>>("breeds");
        }
        catch (Exception ex)
        {
            _logger.LogCritical($"Unable to retrieve Breed list from Cat API: {ex.Message}");
            // Prevent the API from starting up if it fails to retrieve the list of Breeds from the external API
            Environment.FailFast(ex.Message);
        }
    }
}
