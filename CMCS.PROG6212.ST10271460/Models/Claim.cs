namespace CMCS.PROG6212.ST10271460.Models
{
    public class Claim
    {
        public int ClaimID { get; set; } // Primary Key
        public int ContractorID { get; set; } // Foreign Key to User

        public DateTime ClaimPeriod { get; set; }
        public double HoursWorked { get; set; }
        public double HourlyRate { get; set; } // Predefined
        public double Expenses { get; set; }
        public double Bonuses { get; set; } // Optional
        public double Deductions { get; set; } // Optional
        public double Amount { get; set; } // Calculated
        public ClaimStatus Status { get; set; } // Pending, Approved, or Rejected
        public DateTime DateSubmitted { get; set; }
    }

    public enum ClaimStatus
    {
        Pending,
        Approved,
        Rejected
    }
}

