<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Mvc;

namespace CMCS.PROG6212.ST10271460.Models
=======
﻿namespace CMCS.PROG6212.ST10271460.Models
>>>>>>> e67e039fed6ea280849229b3d400860b8a52c9b7
{
    public class ClaimViewModel
    {
        public DateTime ClaimPeriod { get; set; }
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
<<<<<<< HEAD
        public IFormFile? Document { get; set; }  // File upload
        public string? AdditionalNotes { get; set; }  // Any extra notes
=======
        public IFormFile? Document { get; set; }
>>>>>>> e67e039fed6ea280849229b3d400860b8a52c9b7
    }
}
