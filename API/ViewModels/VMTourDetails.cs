using System.Collections.Generic;
using Core.DTO;
using Core.Entities;

namespace API.ViewModels
{
    public class VMTourDetails
    {
        public List<TourAgenda> TourAgenda { get; set; }
        public List<ToursAdditionalInformationsDTO> ToursAdditionalInformations { get; set; }
        public List<TourReviewsDTO> TourReviews { get; set; }  
    }
}