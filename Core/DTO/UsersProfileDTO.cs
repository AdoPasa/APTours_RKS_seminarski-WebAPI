using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.DTO
{
    public class UsersProfileDTO
    {
        [Key]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ParentName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public string Username { get; set; }
        public string MembershipType { get; set; }
        public string Nationality { get; set; }
        public string Citizenship { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Title { get; set; }
        public DateTime MembershipDate { get; set; }
        public string Position { get; set; }
        public string Place { get; set; }
        public string Address { get; set; }
    }
}
