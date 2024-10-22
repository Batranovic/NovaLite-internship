using Konteh.BackOfficeApi;
using Konteh.Test.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Konteh.BackOffice.Api.Tests;

public class BackOfficeApiIntegrationTest : BaseIntegrationTest<Program>
{
    public BackOfficeApiIntegrationTest() : base(OnConfiguring)
    {
    }

    private static void OnConfiguring(IServiceCollection services) =>
        services.AddAuthentication("Test").AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
}
