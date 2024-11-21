


namespace CMCS.PROG6212.ST10271460.Models
{
    public class Claim
    {
        public int Id { get; set; } // Primary Key
        public string LecturerId { get; set; } = string.Empty; // Foreign key for Lecturer
        public string LecturerName { get; set; } = string.Empty; // Lecturer's Name
        public DateTime ClaimPeriod { get; set; } // Claim period
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal Amount { get; set; }
        public decimal Overtime { get; set; } // Automated overtime calculation
        public decimal SpecialAllowance { get; set; } // Special allowance
        public DateTime DateSubmitted { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string DocumentPath { get; set; } = string.Empty;
        public ClaimStatus Status { get; set; } = ClaimStatus.Pending; // Enum for claim status
    }
}

















