using MassTransit;
using Microsoft.Extensions.Options;

namespace Konteh.BackOfficeApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddRabbitMq(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<RabbitMqOptions>(
            builder.Configuration.GetSection(nameof(RabbitMQ)));

        builder.Services.AddMassTransit(conf =>
        {
            conf.SetKebabCaseEndpointNameFormatter();
            conf.AddConsumers(typeof(Program).Assembly);

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
