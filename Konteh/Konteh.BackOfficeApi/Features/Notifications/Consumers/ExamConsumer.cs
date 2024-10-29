using Konteh.BackOfficeApi.Features.Notifications.Hubs;
using Konteh.Infrastructure.DTO;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Konteh.BackOfficeApi.Features.Notifications.Consumers;

public class ExamConsumer : IConsumer<GetExamDTO>
{
    private readonly IHubContext<ExamHub> _examHubContext;

    public ExamConsumer(IHubContext<ExamHub> examHubContext)
    {
        _examHubContext = examHubContext;
    }

    public async Task Consume(ConsumeContext<GetExamDTO> context)
    {
        await _examHubContext.Clients.All.SendAsync("ReceiveMessage", context.Message);
    }
}
