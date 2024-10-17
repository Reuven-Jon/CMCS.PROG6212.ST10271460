using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class ClaimHub : Hub
{
    public async Task UpdateClaimStatus(int claimId, string status, string note)
    {
        await Clients.All.SendAsync("ReceiveClaimUpdate", claimId, status, note);
    }
}


