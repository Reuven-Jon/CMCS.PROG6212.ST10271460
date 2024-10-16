using System;

namespace CMCS.PROG6212.ST10271460.Models
{
    public class Claim
    {
        public int ClaimID { get; set; } // Primary Key
        public int ContractorID { get; set; } // Foreign Key to User
        public User? Contractor { get; set; } // Nullable property
        public string? ContractorName { get; set; } // Nullable property

        public DateTime ClaimPeriod { get; set; } // The period for which the claim is made
        public double HoursWorked { get; set; } // Number of hours worked
        public double HourlyRate { get; set; } // Rate per hour
        public double Amount { get; set; } // Total amount calculated

        // Adding the missing properties
        public double Bonuses { get; set; } // Additional bonuses
        public double Expenses { get; set; } // Additional expenses
        public double Deductions { get; set; } // Any deductions

        public ClaimStatus Status { get; set; } = ClaimStatus.Pending; // Defaults to Pending
        public DateTime DateSubmitted { get; set; } = DateTime.Now; // Date of claim submission
    }
}


