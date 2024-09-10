using CMCS.PROG6212.ST10271460.Models;
//Reuven-Jon Kadalie ST10271460


namespace CMCS.PROG6212.ST10271460.Models
{
    public class Approval
    {
        public int ApprovalID { get; set; } // Primary Key
        public int ClaimID { get; set; } // Foreign Key to Claim

        public int CoordinatorID { get; set; } // Foreign Key to User
        public int ManagerID { get; set; } // Foreign Key to User
        public ClaimStatus CoordinatorApprovalStatus { get; set; }
        public ClaimStatus ManagerApprovalStatus { get; set; }
        public DateTime FinalApprovalDate { get; set; } // Set when all approvals are done
    }
}