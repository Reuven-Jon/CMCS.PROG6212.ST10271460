namespace CMCS.PROG6212.ST10271460.Models
{
    public class Document
    {
        public int DocumentID { get; set; } // Primary Key
        public int ClaimID { get; set; } // Foreign Key to Claim
        public string FilePath { get; set; } // File path for uploaded document
    }
}
