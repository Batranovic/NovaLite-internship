using Konteh.FrontOfficeApi;
using Konteh.FrontOfficeApi.Features.Exams.RandomGenerator;
using Konteh.Test.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Konteh.FrontOffice.Api.Tests;

public class FrontOfficeIntegrationTest : BaseIntegrationTest<Program>
{
    public FrontOfficeIntegrationTest() : base(OnConfiguring)
    {
    }

    private static void OnConfiguring(IServiceCollection services) => services.AddSingleton<IRandomGenerator>(provider => new TestRandomGenerator());
}
