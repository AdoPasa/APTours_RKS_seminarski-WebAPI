using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.DTO
{
    public class ToursDTO
    {
        [Key]
        public int TourID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int NumberOfReviews { get; set; }
        public decimal Grade { get; set; }
        public bool Favorite { get; set; }
        public bool CanAddReview { get; set; }

        public int TourReservationID { get; set; }
        public int NumberOfPassengers { get; set; }
        public DateTime? ReservedAt { get; set; }
        public string ReservedAtString { get; set; }
        public DateTime? UpcomingDate { get; set; }
        public string UpcomingDateString { get; set; }
    }
}
