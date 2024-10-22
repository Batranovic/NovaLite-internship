using Konteh.Domain;
using MassTransit;

namespace Konteh.BackOfficeApi.Features.Notifications.Consumers
{
    public class ExamConsumer : IConsumer<Candidate>
    {
        private readonly ILogger<ExamConsumer> _logger;

        public ExamConsumer(ILogger<ExamConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Candidate> context)
        {
            _logger.LogInformation("Candidate Name:"+  context.Message.Name + " Faculty: " + context.Message.Faculty);
            return Task.CompletedTask;
        }

    }
}
