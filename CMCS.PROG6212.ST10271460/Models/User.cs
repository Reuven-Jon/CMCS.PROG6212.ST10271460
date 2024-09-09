namespace CMCS.PROG6212.ST10271460.Models
{
    public class User
    {
        public int UserID { get; set; } // Primary Key
        public string Name { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; } // Nullable for contractors
        public Role Role { get; set; } // Contractor or Admin
    }

    public enum Role
    {
        Contractor,
        Admin
    }
} 