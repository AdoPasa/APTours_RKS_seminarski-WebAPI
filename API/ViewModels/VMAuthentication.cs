using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.ViewModels
{
    public class VMAuthentication
    {
        public int UserID;
        public string ProfilePhoto;
        public string FirstName;
        public string LastName;
        public string Email { get; set; }
        public string Username;
        public string AuthToken;
    }
}