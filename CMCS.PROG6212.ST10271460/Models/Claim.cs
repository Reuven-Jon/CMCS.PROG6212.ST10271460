using CMCS.PROG6212.ST10271460.Models;

namespace CMCS.PROG6212.ST10271460.Models
{
    public class Claim
    {
        public int ClaimID { get; set; } // Primary Key
        public int ContractorID { get; set; } // Foreign Key to User
        public User? Contractor { get; set; } // Reference to the User class (Contractor)

        public string? ContractorName { get; set; } // Nullable string


        public DateTime ClaimPeriod { get; set; }
        public double HoursWorked { get; set; }
        public double HourlyRate { get; set; } // Predefined
        public double Expenses { get; set; }
        public double Bonuses { get; set; } // Optional
        public double Deductions { get; set; } // Optional
        public double Amount { get; set; } // Calculated
        public ClaimStatus Status { get; set; }
        public DateTime DateSubmitted { get; set; }
    }
}

public enum ClaimStatus
    {
        Pending,
        Approved,
        Rejected
    }

