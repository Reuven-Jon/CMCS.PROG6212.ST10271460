using CMCS.PROG6212.ST10271460.Models;



namespace CMCS.PROG6212.ST10271460.Models
{
    public class User
    {
        public int UserID { get; set; } // Primary Key
        public string Name { get; set; } = string.Empty; // Default value
        public string Email { get; set; } = string.Empty; // Default value
        public string CompanyName { get; set; } = string.Empty; // Default value for contractors
        public Role Role { get; set; } // Contractor or Admin
    }
}


public enum Role
    {
        Contractor,
        Admin
    }
