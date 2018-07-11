using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTO
{
    public class UsersDTO
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string ParentName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public bool Active { get; set; }
        public int NumberOfRows { get; set; }
    }
}
