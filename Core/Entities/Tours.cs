using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Tours
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TourID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = nameof(Resources.Resource.ErrMsgTitleReq))]
        [StringLength(200, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = nameof(Resources.Resource.ErrMsgTitleLen200))]
        public string Title { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = nameof(Resources.Resource.ErrMsgValueLen100))]
        public string Image { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = nameof(Resources.Resource.ErrMsgDurationInDaysReq))]
        public int DurationInDays { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = nameof(Resources.Resource.ErrMsgNumberOfPlacesReq))]
        public int NumberOfPlaces { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = nameof(Resources.Resource.ErrMsgPriceReq))]
        public decimal Price { get; set; }

        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }

        public int GradeSum { get; set; }
        public int NumberOfReviews { get; set; }
        public decimal Grade { get; set; }
    }
}
