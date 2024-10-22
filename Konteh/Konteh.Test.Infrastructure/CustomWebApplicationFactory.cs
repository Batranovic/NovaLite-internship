using Konteh.FrontOfficeApi.Features.Exams.RandomGenerator;
using Konteh.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;

namespace Konteh.Test.Infrastructure;

public class CustomWebApplicationFactory<TProgram>
: WebApplicationFactory<TProgram> where TProgram : class
{
    private Action<IServiceCollection> OnConfiguring { get; set; }

    public CustomWebApplicationFactory(Action<IServiceCollection> onConfiguring)
    {
        OnConfiguring = onConfiguring;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<AppDbContext>));

            if (dbContextDescriptor != null)
            {
                services.Remove(dbContextDescriptor);
            }

            var dbConnectionDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbConnection));

            if (dbConnectionDescriptor != null)
            {
                services.Remove(dbConnectionDescriptor);
            }
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IRandomGenerator));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            OnConfiguring(services);

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("Server=.;Database=KontehTest;Trusted_Connection=True;TrustServerCertificate=True;");
            });

            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();
        });
    }
}
