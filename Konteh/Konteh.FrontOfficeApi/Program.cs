using FluentValidation;
using Konteh.Domain;
using Konteh.FrontOfficeApi.Features.Exams.RandomGenerator;
using Konteh.FrontOfficeApi.Options;
using Konteh.Infrastructure;
using Konteh.Infrastructure.ExceptionHandlers;
using Konteh.Infrastructure.Extensions;
using Konteh.Infrastructure.PipelineBehaviours;
using Konteh.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Konteh.FrontOfficeApi;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);



        // Add services to the container.

        builder.AddRabbitMq(Assembly.GetExecutingAssembly());
        builder.Services.AddControllers();
        builder.Services.AddOpenApiDocument(o => o.SchemaSettings.SchemaNameGenerator = new CustomSwaggerSchemaNameGenerator());
        builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<IRandomGenerator, RandomGenerator>();
        builder.Services.AddScoped<IRepository<Question>, QuestionRepository>();
        builder.Services.AddScoped<IRepository<Exam>, ExamRepository>();
        builder.Services.AddScoped<IRepository<Candidate>, CandidateRepository>();
        builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
        builder.Services.Configure<ExamOptions>(builder.Configuration.GetSection(ExamOptions.SectionName));

        builder.Services.AddMediatR(cfg =>
      {
          cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
          cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
      });
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


        // Added for exceptions handling
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("MyCorsPolicy", corsBuilder =>
            {
                corsBuilder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseHttpsRedirection();
        app.UseExceptionHandler();
        app.UseOpenApi();
        app.UseSwaggerUi();

        app.UseAuthorization();
        app.UseCors("MyCorsPolicy");
        app.MapControllers();

        app.Run();
    }
}
