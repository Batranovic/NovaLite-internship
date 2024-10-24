using Konteh.Domain;
using Konteh.FrontOfficeApi.Features.Exams.RandomGenerator;
using Konteh.Infrastructure;
using Konteh.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Konteh.FrontOfficeApi;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        builder.Services.AddScoped<IRandomGenerator, RandomGenerator>();
        builder.Services.AddScoped<IRepository<Question>, QuestionRepository>();
        builder.Services.AddScoped<IRepository<Exam>, ExamRepository>();

        rabbitMqSetup(builder);

        builder.Services.AddControllers();
        builder.Services.AddOpenApiDocument(o => o.SchemaSettings.SchemaNameGenerator = new CustomSwaggerSchemaNameGenerator());
        builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("MyCorsPolicy", corsBulder =>
            {
                corsBulder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseOpenApi();
        app.UseSwaggerUi();
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void rabbitMqSetup(WebApplicationBuilder builder)
    {
        var rabbitMqSettings = builder.Configuration.GetSection("RabbitMQ");
        builder.Services.AddMassTransit(conf =>
        {
            conf.SetKebabCaseEndpointNameFormatter();
            conf.SetInMemorySagaRepositoryProvider();
            conf.AddConsumers(typeof(Program).Assembly);

            conf.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMqSettings["Host"], "/", h =>
                {
                    h.Username(rabbitMqSettings["Username"] ?? "");
                    h.Password(rabbitMqSettings["Password"] ?? "");
                });

                cfg.ConfigureEndpoints(context);
            });
        });
    }
}
