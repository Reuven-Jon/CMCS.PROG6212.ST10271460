namespace CMCS.PROG6212.ST10271460.Models
{
    public class ClaimViewModel
    {
        public DateTime ClaimPeriod { get; set; }
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public IFormFile? Document { get; set; }
    }
}
