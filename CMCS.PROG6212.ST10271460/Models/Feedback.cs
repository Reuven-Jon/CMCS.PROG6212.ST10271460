namespace CMCS.PROG6212.ST10271460.Models
{
    public class Feedback
    {
        public int FeedbackID { get; set; } // Primary Key
        public int UserID { get; set; } // Foreign key for User
        public string Comments { get; set; } = string.Empty; // Feedback comments
        public DateTime DateSubmitted { get; set; } // When feedback was submitted
    }
}

