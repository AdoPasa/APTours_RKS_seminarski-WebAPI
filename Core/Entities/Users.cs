using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Core.Resources;

namespace Core.Entities
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.ErrMsgFirstNameReq))]
        [StringLength(50, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.ErrMsgFirstNameLen50))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.ErrMsgLastNameReq))]
        [StringLength(50, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.ErrMsgLastNameLen50))]
        public string LastName { get; set; }

        public string ProfilePhoto { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.ErrMsgUserNameReq))]
        [StringLength(100, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.ErrMsgUserNameLen100))]
        public string Username { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.ErrMsgEmailReq))]
        [StringLength(100, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.ErrMsgEmailLen100))]
        public string Email { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.ErrMsgPasswordSaltLen100))]
        public string PasswordSalt { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.ErrMsgPasswordHashLen100))]
        public string PasswordHash { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.ErrMsgGenderReq))]
        [StringLength(10, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.ErrMsgGenderLen10))]
        public string Gender { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.ErrMsgCreatedDateReq))]
        public DateTime CreatedDate { get; set; }
        public bool Active { get; set; }

        public int NumberOfReviews { get; set; }
        public int NumberOfTours { get; set; }

        [Required]
        public string Phone { get; set; }

        public string ActivationCode { get; set; }

        public DateTime? LastAccess { get; set; }

        public DateTime? LastPasswordChange { get; set; }

        public string ResetPasswordToken { get; set; }

        public DateTime? ResetPasswordTokenExpiration { get; set; }
    }
}
