using System;

namespace CMCS.PROG6212.ST10271460.Models
{
    public class Claim
    {
        public int Id { get; set; } // Primary Key
        public string? ContractorName { get; set; } // Lecturer Name
        public DateTime ClaimPeriod { get; set; }
        public double HoursWorked { get; set; }
        public double HourlyRate { get; set; }
        public double Amount { get; set; }
        public ClaimStatus Status { get; set; } = ClaimStatus.Pending; // Defaults to Pending
        public DateTime DateSubmitted { get; set; } = DateTime.Now;

        // Add this property for the uploaded document
        public string? DocumentPath { get; set; } // Path to the uploaded PDF document
    }

}


