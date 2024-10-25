using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Konteh.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddRabbitMq(this WebApplicationBuilder builder, Assembly assembly)
    {
        builder.Services.Configure<RabbitMqOptions>(
            builder.Configuration.GetSection(RabbitMqOptions.RabbitMq));

        builder.Services.AddMassTransit(conf =>
        {
            conf.SetKebabCaseEndpointNameFormatter();
            conf.AddConsumers(assembly);

            conf.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqOptions = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;

                cfg.Host(rabbitMqOptions.Host, "/", h =>
                {
                    h.Username(rabbitMqOptions.Username ?? "");
                    h.Password(rabbitMqOptions.Password ?? "");
                });

                cfg.ConfigureEndpoints(context);
            });
        });
    }
}
