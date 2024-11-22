using System;
using System.ComponentModel.DataAnnotations;

namespace CMCS.PROG6212.ST10271460.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; } // ID of the user submitting feedback

        [Required]
        [StringLength(1000, ErrorMessage = "Feedback content cannot exceed 1000 characters.")]
        public string Content { get; set; } = string.Empty;

        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}


