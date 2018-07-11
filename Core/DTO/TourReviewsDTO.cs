using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
    public class TourReviewsDTO
    {
        [Key]
        public int TourReviewID { get; set; }
        public string User { get; set; }
        public string ProfilePhoto { get; set; }
        public int Grade { get; set; }
        public string Review { get; set; }
        public DateTime CreatedAtDate { get; set; }
        public string CreatedAt { get; set; }
    }
}
