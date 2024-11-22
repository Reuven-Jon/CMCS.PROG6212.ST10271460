namespace CMCS.PROG6212.ST10271460.Models
{
    public class Lecturer
    {
        public int Id { get; set; } // Primary Key
        public required string Name { get; set; } // Name of the Lecturer
        public required string Email { get; set; } // Lecturer Email
        public decimal HourlyRate { get; set; } // Hourly Rate
    }
}


