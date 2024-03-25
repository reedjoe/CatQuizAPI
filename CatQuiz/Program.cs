using CatQuiz.Core.Exceptions;
using CatQuiz.Core.Http;
using CatQuiz.Data;
using CatQuiz.Shared.Breeds;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CatApiSettings>(builder.Configuration.GetSection("CatApiSettings"));

var connectionString = "DataSource=catquizdb;mode=memory;cache=shared";
var sqliteConnection = new SqliteConnection(connectionString);
sqliteConnection.Open();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(connectionString);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("CatApi", (serviceProvider, client) =>
{
    var settings = serviceProvider
        .GetRequiredService<IOptions<CatApiSettings>>().Value;

    client.DefaultRequestHeaders.Add("x-api-key", settings.ApiKey);
    client.BaseAddress = new Uri(settings.BaseUrl);
})
    .AddPolicyHandler(HttpUtils.GetRetryPolicy())
    .AddPolicyHandler(HttpUtils.GetCircuitBreakerPolicy());
builder.Services.AddSingleton<IBreedProvider, BreedProvider>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    await dataContext.Database.EnsureCreatedAsync();
    await new DataSeed().SeedData(dataContext);

    var breedProvider = scope.ServiceProvider.GetRequiredService<IBreedProvider>();
    await breedProvider.LoadBreeds();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
