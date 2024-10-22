using Konteh.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Respawn;

namespace Konteh.Test.Infrastructure;

public abstract class BaseIntegrationTest<TProgram> : IDisposable where TProgram : class
{
    protected readonly CustomWebApplicationFactory<TProgram> _factory;
    protected HttpClient _httpClient;
    protected Respawner _respawner;
    protected static string _connection = "Server=.;Database=KontehTest;Trusted_Connection=True;TrustServerCertificate=True;";

    public BaseIntegrationTest(Action<IServiceCollection> onConfiguring)
    {
        _factory = new CustomWebApplicationFactory<TProgram>(onConfiguring);
    }

    [SetUp]
    public virtual async Task InitializeAsync()
    {
        _httpClient = _factory.CreateClient();
        ConfigureHttpClient(_httpClient);

        _respawner = await Respawner.CreateAsync(_connection, new RespawnerOptions
        {
            TablesToIgnore = ["__EFMigrationsHistory"]
        });
        await _respawner.ResetAsync(_connection);

        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            SeedDatabase(db);
            await db.SaveChangesAsync();
        }
    }
    protected virtual void ConfigureHttpClient(HttpClient httpClient)
    {

    }

    [TearDown]
    public void Dispose()
    {
        _httpClient.Dispose();
        _factory.Dispose();
    }

    protected static void SeedDatabase(AppDbContext db)
    {
        var questions = TestData.GetAllQuestions();
        db.Questions.AddRange(questions);
    }
}
