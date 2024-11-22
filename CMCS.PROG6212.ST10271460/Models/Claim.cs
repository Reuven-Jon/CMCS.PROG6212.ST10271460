
using System;
using System.ComponentModel.DataAnnotations;

namespace CMCS.PROG6212.ST10271460.Models
{
    public class Claim
    {
        public int Id { get; set; } // Primary Key

        [Required]
        public int LecturerId { get; set; } // Foreign Key for User

        [Required]
        [StringLength(100, ErrorMessage = "Lecturer name cannot exceed 100 characters.")]
        public string LecturerName { get; set; } = string.Empty; // Lecturer's Name

        [Required]
        public DateTime ClaimPeriod { get; set; } // Claim period

        [Range(0, int.MaxValue, ErrorMessage = "Hours worked must be a positive value.")]
        public int HoursWorked { get; set; } // Hours worked

        [Range(0.0, double.MaxValue, ErrorMessage = "Hourly rate must be a positive value.")]
        public decimal HourlyRate { get; set; } // Hourly rate

        [Range(0.0, double.MaxValue, ErrorMessage = "Amount must be a positive value.")]
        public decimal Amount { get; set; } // Total claim amount

        [Range(0.0, double.MaxValue, ErrorMessage = "Overtime must be a positive value.")]
        public decimal Overtime { get; set; } // Automated overtime calculation

        [Range(0.0, double.MaxValue, ErrorMessage = "Special allowance must be a positive value.")]
        public decimal SpecialAllowance { get; set; } // Special allowance

        [Required]
        public DateTime DateSubmitted { get; set; } // Submission date

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string Notes { get; set; } = string.Empty; // Additional notes

        [StringLength(255, ErrorMessage = "Document path cannot exceed 255 characters.")]
        public string DocumentPath { get; set; } = string.Empty; // Path to uploaded document

        [Required]
        public ClaimStatus Status { get; set; }  // Enum for claim status

        // Navigation Property (if needed)
        public User? Lecturer { get; set; } // Navigates to User (optional)
    }
}

// Move the enum to a separate file or ensure it is not duplicated in the same namespace
public enum ClaimStatus
{
    Pending,
    Approved,
    Rejected
}









