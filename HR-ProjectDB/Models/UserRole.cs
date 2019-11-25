using System;
using System.Collections.Generic;

namespace HR_ProjectDB.Models
{
    public partial class UserRole
    {
        public UserRole()
        {
            User = new HashSet<User>();
        }

        public int IdUserRole { get; set; }
        public string Role { get; set; }

        public ICollection<User> User { get; set; }
    }
}
