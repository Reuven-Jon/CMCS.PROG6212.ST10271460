using CMCS.PROG6212.ST10271460.Models;
//Reuven-Jon Kadalie ST10271460


namespace CMCS.PROG6212.ST10271460.Models
{
    public class User
    {
        public int UserID { get; set; } // Primary Key
        public string Name { get; set; } = string.Empty; // Default value
        public string Email { get; set; } = string.Empty; // Default value
        public string Password { get; set; } = string.Empty; // New Password field
        public string CompanyName { get; set; } = string.Empty; // Default value for contractors
        public Role Role { get; set; } // Role enum
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
