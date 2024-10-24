using Konteh.BackOfficeApi.Features.Notifications.Hubs;
using Konteh.Domain;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Konteh.BackOfficeApi.Features.Notifications.Consumers;

public class ExamConsumer : IConsumer<Candidate>
{
    private readonly IHubContext<ExamHub> _examHubContext;

    public ExamConsumer(IHubContext<ExamHub> examHubContext)
    {
        _examHubContext = examHubContext;
    }

    public async Task Consume(ConsumeContext<Candidate> context)
    {
        await _examHubContext.Clients.All.SendAsync("ReceiveMessage", context.Message.Name);
    }
}
