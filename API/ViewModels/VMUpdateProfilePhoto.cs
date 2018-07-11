using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.ViewModels
{
    public class VMUpdateProfilePhoto
    {
        public string ImageName { get; set; }
        public byte[] ImageStream{ get; set; }
    }
}