using Konteh.Domain;
using Konteh.Domain.Enumerations;
using Konteh.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using System.Net.Http.Headers;

namespace Konteh.FrontOffice.Api.Tests
{
    public abstract class BaseIntegrationTest<TProgram> : IDisposable where TProgram : class
    {
        protected readonly CustomWebApplicationFactory<TProgram> _factory;
        protected HttpClient _httpClient;
        protected static Respawner _respawner;
        protected static string _connection = "Server=.;Database=KontehTest;Trusted_Connection=True;TrustServerCertificate=True;";

        public BaseIntegrationTest()
        {
            _factory = new CustomWebApplicationFactory<TProgram>();
        }

        [SetUp]
        public virtual async Task InitializeAsync()
        {
            _httpClient = _factory.CreateClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

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

        [TearDown]
        public void Dispose()
        {
            _httpClient.Dispose();
            _factory.Dispose();
        }

        protected static void SeedDatabase(AppDbContext db)
        {
            var questions = new List<Question>
            {
                new() {
                    Text = "What is an interface?",
                    Category = QuestionCategory.General,
                    Answers =
                    [
                        new() { Text = "A class that can have multiple methods"},
                        new() { Text = "A reference type used to define a contract for classes"},
                        new() { Text = "A class with no constructors"},
                        new() { Text = "A static class" }
                    ]
                },
                new() {
                    Text = "What is abstraction?",
                    Category = QuestionCategory.General,
                    Answers =
                    [
                        new() { Text = "Hiding complexity and showing only the essential features of an object"},
                        new() { Text = "Providing access to all details of an object"},
                        new() { Text = "Combining multiple classes into one"},
                    ]
                },
                new() {
                    Text = "What is inheritance?",
                    Category = QuestionCategory.OOP,
                    Answers =
                    [
                        new() { Text = "The ability of a class to inherit features from another class"},
                        new() { Text = "The ability to encapsulate data" },
                        new() { Text = "The ability to create objects" }
                    ]
                },
                new() {
                    Text = "What is the purpose of a constructor?",
                    Category = QuestionCategory.General,
                    Answers =
                    [
                        new() { Text = "To initialize the state of an object"},
                        new() { Text = "To define methods for the class"},
                        new() { Text = "To inherit properties from another class" }
                    ]
                },
                new() {
                    Text = "What is encapsulation?",
                    Category = QuestionCategory.OOP,
                    Answers =
                    [
                        new() { Text = "Hiding internal details of an object"},
                        new() { Text = "Allowing objects to inherit from other objects"},
                        new() { Text = "Separating object concerns" }
                    ]
                },
            };
            db.Questions.AddRange(questions);
        }
    }
}