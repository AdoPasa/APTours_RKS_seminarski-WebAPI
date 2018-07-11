using Core.Resources;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels
{
    public class VMUpdateProfile
    {
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.ErrMsgFirstNameReq))]
        [StringLength(50, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.ErrMsgFirstNameLen50))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.ErrMsgLastNameReq))]
        [StringLength(50, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.ErrMsgLastNameLen50))]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.ErrMsgEmailReq))]
        [StringLength(100, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.ErrMsgEmailLen100))]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.ErrMsgPasswordRequired))]
        [RegularExpression(@"^((?=.*[a-zA-Z])(?=.*\d)).{6,}$", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.ErrMsgPasswordComplexity))]
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.ErrMsgGenderReq))]
        public string Gender { get; set; }
    }
}