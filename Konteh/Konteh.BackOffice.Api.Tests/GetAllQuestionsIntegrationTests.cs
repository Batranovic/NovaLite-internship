using Konteh.BackOfficeApi;
using Konteh.BackOfficeApi.Features.Questions;
using Konteh.FrontOffice.Api.Tests;
using Newtonsoft.Json;
using System.Net;

namespace Konteh.BackOffice.Api.Tests
{
    public class GetAllQuestionsIntegrationTests : BaseIntegrationTest<Program>
    {
        [Test]
        public async Task Handle_ShouldGetAllQuestions()
        {
            var response = await _httpClient.GetAsync("/questions");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response, Is.Not.Null);

            var jsonContent = await response.Content.ReadAsStringAsync();
            var questions = JsonConvert.DeserializeObject<IEnumerable<GetAllQuestions.Response>>(jsonContent);
            Assert.That(questions, Is.Not.Null);
            Assert.That(questions.Count(), Is.GreaterThan(0));
            await Verify(questions).IgnoreMembers("Id");
        }
    }
}