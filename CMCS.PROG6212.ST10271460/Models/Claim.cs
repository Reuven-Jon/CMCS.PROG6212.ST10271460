using System;

namespace CMCS.PROG6212.ST10271460.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public int ContractorID { get; set; }
        public User? Contractor { get; set; }
        public string? ContractorName { get; set; }
        public DateTime ClaimPeriod { get; set; }
        public double HoursWorked { get; set; }
        public double HourlyRate { get; set; }
        public double Amount { get; set; }
        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;
        public DateTime DateSubmitted { get; set; } = DateTime.Now;

        // Field for storing the uploaded document path
        public string? DocumentPath { get; set; } // Optional document upload
    }
}


