using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class Enumerations
    {
        public enum Roles
        {
            User = 1,
            Moderator,
            Administrator
        };

        public enum ContactTypes {
            Email = 1,
            Telephone,
            Fax
        }
    }
}
