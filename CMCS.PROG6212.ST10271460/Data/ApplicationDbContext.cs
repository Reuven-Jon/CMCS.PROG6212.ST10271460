using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CMCS.PROG6212.ST10271460.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public new DbSet<User> Users { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Approval> Approvals { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }

    }

}

