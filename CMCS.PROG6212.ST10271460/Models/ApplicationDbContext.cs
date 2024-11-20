using CMCS.PROG6212.ST10271460.Models;
using Microsoft.EntityFrameworkCore;

namespace CMCS.PROG6212.ST10271460.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Claim> Claims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<HR> HRs { get; set; }
        public DbSet<AcademicManager> AcademicManagers { get; set; }
    }
}
