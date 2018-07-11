using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.DTO
{
    public class ToursAdditionalInformationsDTO
    {
        [Key]
        public int TourAdditionalInformationID { get; set; }

        public string AdditionalInformationType { get; set; }
        public string Value { get; set; }
    }
}
