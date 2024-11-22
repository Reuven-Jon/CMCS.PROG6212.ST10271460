using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using CMCS.PROG6212.ST10271460.Models;


namespace CMCS.PROG6212.ST10271460.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // Ensure the database is created
            context.Database.EnsureCreated();

            // Seed Roles
            string[] roles = { "Lecturer", "HR", "AcademicManager" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Seed Users
            var lecturerUser = new IdentityUser { UserName = "lecturer@example.com", Email = "lecturer@example.com" };
            if (!userManager.Users.Any(u => u.UserName == lecturerUser.UserName))
            {
                var result = await userManager.CreateAsync(lecturerUser, "Lecturer@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(lecturerUser, "Lecturer");
                }
            }

            var hrUser = new IdentityUser { UserName = "hr@example.com", Email = "hr@example.com" };
            if (!userManager.Users.Any(u => u.UserName == hrUser.UserName))
            {
                var result = await userManager.CreateAsync(hrUser, "HR@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(hrUser, "HR");
                }
            }

            var managerUser = new IdentityUser { UserName = "manager@example.com", Email = "manager@example.com" };
            if (!userManager.Users.Any(u => u.UserName == managerUser.UserName))
            {
                var result = await userManager.CreateAsync(managerUser, "Manager@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(managerUser, "AcademicManager");
                }
            }

            // Seed Claims
            if (!context.Claims.Any())
            {
                context.Claims.AddRange(
                    new Claim
                    {
                        LecturerName = "lecturer@example.com",
                        ClaimPeriod = new DateTime(2024, 10, 1),
                        HoursWorked = 40,
                        HourlyRate = 150,
                        Amount = 6000,
                        Status = Models.ClaimStatus.Pending,
                        DateSubmitted = DateTime.Now
                    },
                    new Claim
                    {
                        LecturerName = "lecturer@example.com",
                        ClaimPeriod = new DateTime(2024, 9, 1),
                        HoursWorked = 35,
                        HourlyRate = 140,
                        Amount = 4900,
                        Status = Models.ClaimStatus.Approved,
                        DateSubmitted = DateTime.Now
                    }
                );
                context.SaveChanges();
            }
        }
    }
}


