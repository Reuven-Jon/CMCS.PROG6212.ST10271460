using Microsoft.AspNetCore.SignalR;

namespace CMCS.PROG6212.ST10271460.Hubs
{
    public class NotificationHub : Hub
    {
        // This method can be called by clients to send a message to all connected clients
        public async Task SendClaimUpdate(int claimId, string status, string notes)
        {
            await Clients.All.SendAsync("ReceiveClaimUpdate", claimId, status, notes);
        }
    }
}

