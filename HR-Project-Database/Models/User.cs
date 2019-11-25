using System;
using System.Collections.Generic;

namespace HR_Project_Database.Models
{
    public partial class User
    {
        public User()
        {
            Application = new HashSet<Application>();
            Responsibility = new HashSet<Responsibility>();
        }

        public int IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public int UserProfileId { get; set; }

        public ICollection<Application> Application { get; set; }
        public ICollection<Responsibility> Responsibility { get; set; }
    }
}
