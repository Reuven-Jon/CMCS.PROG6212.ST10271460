using System.ComponentModel.DataAnnotations;
using CMCS.PROG6212.ST10271460.Models;

namespace CMCS.PROG6212.ST10271460.Models { 

public class ClaimViewModel
{
    [Required]
    public DateTime ClaimPeriod { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Hours worked must be greater than zero.")]
    public int HoursWorked { get; set; }

    [Required]
    [Range(1, double.MaxValue, ErrorMessage = "Hourly rate must be greater than zero.")]
    public decimal HourlyRate { get; set; }

    [Range(0.0, double.MaxValue)]
    public decimal SpecialAllowance { get; set; }

    public IFormFile? Document { get; set; }

    [StringLength(500)]
    public string? AdditionalNotes { get; set; }
}

}


