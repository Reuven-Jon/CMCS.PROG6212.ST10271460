namespace CMCS.PROG6212.ST10271460.Models
{
    public enum ClaimStatus
    {
        Pending,    // Default state when the claim is first submitted
        Approved,   // When the claim has been approved by all levels
        Rejected,   // When the claim has been rejected
        Submitted   // Claim submitted but not yet approved
    }
}


