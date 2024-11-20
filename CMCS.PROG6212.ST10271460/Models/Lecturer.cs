namespace CMCS.PROG6212.ST10271460.Models
{
    public class Lecturer
    {
        public int LecturerID { get; set; }
        public string FirstName { get; set; } = string.Empty; // Default value
        public string LastName { get; set; } = string.Empty;  // Default value
        public string Email { get; set; } = string.Empty;     // Default value
        public string PhoneNumber { get; set; } = string.Empty; // Default value
        public decimal HourlyRate { get; set; } = 0.0m;       // Default value for HourlyRate
    }
}


