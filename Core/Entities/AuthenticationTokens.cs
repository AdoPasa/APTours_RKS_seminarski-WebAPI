using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    [Table("AuthenticationTokens")]
    public class AuthenticationTokens
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthenticationTokenID { get; set; }

        public int UserID { get; set; }

        public string AuthenticationToken { get; set; }

        public string DeviceToken { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public string Info_Version_Release { get; set; }

        public string Info_Device { get; set; }

        public string Info_Model { get; set; }

        public string Info_Product { get; set; }

        public string Info_Manufacturer { get; set; }

        public string Info_Brand { get; set; }

        public string Android_SerialOrID { get; set; }

        public DateTime? DateTimeDeleted { get; set; }

        public bool IsDeleted { get; set; }
    }
}
