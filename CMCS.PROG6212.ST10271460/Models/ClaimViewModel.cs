using Microsoft.AspNetCore.Http;

namespace CMCS.PROG6212.ST10271460.Models
{
    public class ClaimViewModel
    {
        public DateTime ClaimPeriod { get; set; }
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal Overtime { get; set; } // For automated calculation display
        public decimal SpecialAllowance { get; set; } // Display special allowances
        public IFormFile? Document { get; set; }  // Allow multiple file types for upload
        public string? AdditionalNotes { get; set; }  // Any extra notes
    }
}


