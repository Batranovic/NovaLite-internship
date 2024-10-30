namespace Konteh.Infrastructure.Extensions;

class RabbitMqOptions
{
    public static string RabbitMq = "RabbitMq";

    public string Host { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
