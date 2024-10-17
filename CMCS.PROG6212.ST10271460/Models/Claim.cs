using System;

namespace CMCS.PROG6212.ST10271460.Models
{

    public class Claim
    {
        public int Id { get; set; } // Primary Key
        public string? LecturerId { get; set; } // Foreign key for Lecturer
        public string? LecturerName { get; set; } // Lecturer's Name
        public DateTime ClaimPeriod { get; set; } // Claim period
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string? Status { get; set; }  // "Pending", "Approved", "Rejected"
        public string? DocumentPath { get; set; } // Path to the uploaded document
        public string? Notes { get; set; } // Additional notes
    }

}














