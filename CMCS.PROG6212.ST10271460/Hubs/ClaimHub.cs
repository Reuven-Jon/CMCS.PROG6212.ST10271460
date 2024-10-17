using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class ClaimHub : Hub
{
    public async Task SendClaim(string lecturerName, string claimData, string notes)
    {
        // Notify all connected clients about the new claim submission
        await Clients.All.SendAsync("ReceiveClaim", lecturerName, claimData, notes);
    }

    public async Task UpdateClaimStatus(string claimId, string status, string managerNote)
    {
        // Notify all connected clients about the updated claim status
        await Clients.All.SendAsync("ReceiveClaimStatusUpdate", claimId, status, managerNote);
    }
}

