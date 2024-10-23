using Konteh.BackOfficeApi.Features.Notifications.Hubs;
using Konteh.Domain;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Konteh.BackOfficeApi.Features.Notifications.Consumers
{
    public class ExamConsumer : IConsumer<Candidate>
    {
        private readonly ILogger<ExamConsumer> _logger;
        private readonly IHubContext<ExamHub> _examHubContext;

        public ExamConsumer(ILogger<ExamConsumer> logger, IHubContext<ExamHub> examHubContext)
        {
            _logger = logger;
            _examHubContext = examHubContext;
        }

        public async Task Consume(ConsumeContext<Candidate> context)
        {
            _logger.LogInformation("Candidate Name: " + context.Message.Name + " Faculty: " + context.Message.Faculty);
            await _examHubContext.Clients.All.SendAsync("ReceiveMessage", context.Message.Name);
        }
    }
}
