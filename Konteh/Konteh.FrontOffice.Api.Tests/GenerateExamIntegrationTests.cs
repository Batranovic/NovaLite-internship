using Konteh.Domain;
using Konteh.Domain.Enumerations;
using Konteh.FrontOfficeApi.Features.Exams;
using Konteh.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Respawn;
using System.Net;
using System.Net.Http.Json;

namespace Konteh.FrontOffice.Api.Tests
{
    public class GenerateExamIntegrationTests : IDisposable
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private HttpClient _httpClient;
        private static Respawner _respawner;
        private static string _connection = "Server=.;Database=KontehTest;Trusted_Connection=True;TrustServerCertificate=True;";

        public GenerateExamIntegrationTests()
        {
            _factory = new CustomWebApplicationFactory<Program>();
        }

        [SetUp]
        public async Task InitializeAsync()
        {
            _httpClient = _factory.CreateClient();
            _respawner = await Respawner.CreateAsync(_connection, new RespawnerOptions
            {
                TablesToIgnore = ["__EFMigrationsHistory"]
            });
            await _respawner.ResetAsync(_connection);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
            _factory.Dispose();
        }

        [Test]
        public async Task Handle_ShouldCreateExam()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                SeedDatabase(db);
                await db.SaveChangesAsync();
            }

            var command = new GenerateExam.Command { QuestionPerCategory = 2 };

            var response = await _httpClient.PostAsJsonAsync("/exams", command);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response, Is.Not.Null);

            var jsonContent = await response.Content.ReadAsStringAsync();
            var exam = JsonConvert.DeserializeObject<GenerateExam.Response>(jsonContent);
            await Verify(exam);

        }
        private void SeedDatabase(AppDbContext db)
        {
            var questions = new List<Question>
            {
                new Question
                {
                    Text = "What is an interface?",
                    Category = QuestionCategory.General,
                    Answers = new List<Answer>
                    {
                        new Answer { Text = "A class that can have multiple methods"},
                        new Answer { Text = "A reference type used to define a contract for classes"},
                        new Answer { Text = "A class with no constructors"},
                        new Answer { Text = "A static class" }
                    }
                },
                new Question
                {
                    Text = "What is abstraction?",
                    Category = QuestionCategory.General,
                    Answers = new List<Answer>
                    {
                        new Answer { Text = "Hiding complexity and showing only the essential features of an object"},
                        new Answer { Text = "Providing access to all details of an object"},
                        new Answer { Text = "Combining multiple classes into one"},
                    }
                },
                new Question
                {
                    Text = "What is inheritance?",
                    Category = QuestionCategory.OOP,
                    Answers = new List<Answer>
                    {
                        new Answer { Text = "The ability of a class to inherit features from another class"},
                        new Answer { Text = "The ability to encapsulate data" },
                        new Answer { Text = "The ability to create objects"}
                    }
                },
                new Question
                {
                    Text = "What is the purpose of a constructor?",
                    Category = QuestionCategory.General,
                    Answers = new List<Answer>
                    {
                        new Answer { Text = "To initialize the state of an object"},
                        new Answer { Text = "To define methods for the class"},
                        new Answer { Text = "To inherit properties from another class"}
                    }
                },
                    new Question
                {

                    Text = "What is encapsulation?",
                    Category = QuestionCategory.OOP,
                    Answers = new List<Answer>
                    {
                        new Answer { Text = "Hiding internal details of an object"},
                        new Answer { Text = "Allowing objects to inherit from other objects"},
                        new Answer { Text = "Separating object concerns"}
                    }
                },
            };

            db.Questions.AddRange(questions);
        }
    }
}