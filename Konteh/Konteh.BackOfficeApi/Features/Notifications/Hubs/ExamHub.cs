using Microsoft.AspNetCore.SignalR;

namespace Konteh.BackOfficeApi.Features.Notifications.Hubs
{
    public class ExamHub : Hub
    {
        public async Task SendMessae(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
