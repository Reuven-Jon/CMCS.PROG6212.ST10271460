using CMCS.PROG6212.ST10271460.Models;
using Microsoft.EntityFrameworkCore;


namespace CMCS.PROG6212.ST10271460.Models
{
  
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }

            public DbSet<Claim> Claims { get; set; }
            public DbSet<User> Users { get; set; }
            public DbSet<Document> Documents { get; set; }
            public DbSet<Approval> Approvals { get; set; }
        }
    }
