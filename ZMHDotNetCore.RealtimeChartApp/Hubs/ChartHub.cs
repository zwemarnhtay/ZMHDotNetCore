using Microsoft.AspNetCore.SignalR;

namespace ZMHDotNetCore.RealtimeChartApp.Hubs;

public class ChartHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
