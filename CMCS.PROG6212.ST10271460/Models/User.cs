namespace CMCS.PROG6212.ST10271460.Models
{
    public class User
    {
        public int UserID { get; set; } // Primary Key
        public string Name { get; set; } = string.Empty; // Name of the user
        public string Email { get; set; } = string.Empty; // Email of the user
        public string Password { get; set; } = string.Empty; // Password for login
        public string CompanyName { get; set; } = string.Empty; // For contractors
        public Role Role { get; set; } // Role enum
        public string? Rank { get; set; } // Rank for lecturers (e.g., Junior, Senior)
    }

    public enum Role
    {
        Lecturer,
        Manager,
        Coordinator,
        Contractor,
        Admin
    }
}

