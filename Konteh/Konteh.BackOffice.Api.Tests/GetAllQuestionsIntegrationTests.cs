using Konteh.BackOfficeApi.Features.Questions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace Konteh.BackOffice.Api.Tests;

public class GetAllQuestionsIntegrationTests : BackOfficeApiIntegrationTest
{
    protected override void ConfigureHttpClient(HttpClient httpClient)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
    }

    [Test]
    public async Task Handle_ShouldGetAllQuestions()
    {
        var response = await _httpClient.GetAsync("/questions");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response, Is.Not.Null);

        var jsonContent = await response.Content.ReadAsStringAsync();
        var questions = JsonConvert.DeserializeObject<IEnumerable<GetAllQuestions.Response>>(jsonContent);
        await Verify(questions).IgnoreMembers("Id");
    }
}
