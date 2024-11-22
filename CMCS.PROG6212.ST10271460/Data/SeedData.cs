using System;
using CMCS.PROG6212.ST10271460.Models;
using Microsoft.Extensions.DependencyInjection;
using CMCS.PROG6212.ST10271460.Controllers;

namespace CMCS.PROG6212.ST10271460.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetRequiredService<ApplicationDbContext>())
            {
                // Ensure the database is empty before seeding
                if (context.Users.Any() || context.Claims.Any() || context.Feedbacks.Any())
                {
                    return; // Database has already been seeded
                }

                // Seed Users
                context.Users.AddRange(
                              new User { Username = "PEEL", Password = "password1", Role = Role.HR },
                              new User { Username = "JOHN", Password = "password2", Role = Role.Lecturer },
                              new User { Username = "MARK", Password = "password3", Role = Role.AcademicManager }
                          );
                


                // Seed Claims
                context.Claims.AddRange(
                    new Claim { Id = 1, LecturerName = "Lecturer1", ClaimPeriod = new DateTime(2024, 10, 1), HoursWorked = 40, HourlyRate = 150, Amount = 6000, Status = (CMCS.PROG6212.ST10271460.Models.ClaimStatus)ClaimStatus.Pending, DateSubmitted = DateTime.Now },
                    new Claim { Id = 2, LecturerName = "Lecturer1", ClaimPeriod = new DateTime(2024, 9, 1), HoursWorked = 35, HourlyRate = 140, Amount = 4900, Status = (CMCS.PROG6212.ST10271460.Models.ClaimStatus)ClaimStatus.Approved, DateSubmitted = DateTime.Now },
                    new Claim { Id = 3, LecturerName = "Lecturer1", ClaimPeriod = new DateTime(2024, 8, 1), HoursWorked = 20, HourlyRate = 130, Amount = 2600, Status = (CMCS.PROG6212.ST10271460.Models.ClaimStatus)ClaimStatus.Rejected, DateSubmitted = DateTime.Now }
                );

                // Seed Feedback
                context.Feedbacks.AddRange(
                    new Feedback { Id = 1, UserId = 1, Content = "Great system, but the approval process can be faster!", SubmittedAt = DateTime.Now },
                    new Feedback { Id = 2, UserId = 2, Content = "UI could use some improvements for accessibility.", SubmittedAt = DateTime.Now },
                    new Feedback { Id = 3, UserId = 3, Content = "HR needs better tools for managing claims.", SubmittedAt = DateTime.Now }
                );

                // Save Changes
                context.SaveChanges();
            }
        }
    }
}

