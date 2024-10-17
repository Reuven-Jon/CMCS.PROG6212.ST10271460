

<<<<<<< HEAD
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
=======
    using System;

    namespace CMCS.PROG6212.ST10271460.Models
    {
        public class Claim
        {
            public int Id { get; set; } // Primary Key
            public string ContractorName { get; set; } = string.Empty; // Nullable or provide a default value
            public double Bonuses { get; set; } // Additional bonuses
            public double Expenses { get; set; } // Additional expenses
            public double Deductions { get; set; } // Any deductions
            public string LecturerId { get; set; } = string.Empty; // Foreign key for Lecturer
            public string LecturerName { get; set; } = string.Empty; // Lecturer's Name
            public DateTime ClaimPeriod { get; set; } // Claim period
            public int HoursWorked { get; set; }
            public decimal HourlyRate { get; set; }
            public decimal Amount { get; set; }
            public DateTime DateSubmitted { get; set; }
            public string Notes { get; set; } = string.Empty;
            public string DocumentPath { get; set; } = string.Empty;
            public string Status { get; set; } = ClaimStatus.Pending.ToString();
        }
>>>>>>> e67e039fed6ea280849229b3d400860b8a52c9b7
    }

}















