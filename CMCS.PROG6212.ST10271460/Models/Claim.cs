using System;

namespace CMCS.PROG6212.ST10271460.Models
{
   
        public class Claim
        {
            public int Id { get; set; } // Primary Key
            public string ContractorName { get; set; } = string.Empty; // Nullable or provide a default value

            public DateTime ClaimPeriod { get; set; }
            public double HoursWorked { get; set; }
            public double HourlyRate { get; set; }
            public double Amount { get; set; }
            public string? Notes { get; set; }
        public double Bonuses { get; set; } // Additional bonuses
            public double Expenses { get; set; } // Additional expenses
            public double Deductions { get; set; } // Any deductions

            public ClaimStatus Status { get; set; } = ClaimStatus.Pending; // Defaults to Pending
            public DateTime DateSubmitted { get; set; } = DateTime.Now; // Date of claim submission

            public string DocumentPath { get; set; } = string.Empty; // Provide a default value
        }
    }




