using Konteh.FrontOfficeApi;
using Konteh.FrontOfficeApi.Features.Exams;
using Konteh.Test.Infrastructure;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace Konteh.FrontOffice.Api.Tests
{
    public class GenerateExamIntegrationTests : BaseIntegrationTest<Program>
    {
        [Test]
        public async Task Handle_ShouldCreateExam()
        {
            var command = new GenerateExam.Command { QuestionPerCategory = 2 };

            var response = await _httpClient.PostAsJsonAsync("/exams", command);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response, Is.Not.Null);

            var jsonContent = await response.Content.ReadAsStringAsync();
            var exam = JsonConvert.DeserializeObject<GenerateExam.Response>(jsonContent);
            await Verify(exam).IgnoreMembers("Id");
        }
    }
}
