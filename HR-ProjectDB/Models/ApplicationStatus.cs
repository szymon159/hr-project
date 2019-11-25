using System;
using System.Collections.Generic;

namespace HR_ProjectDB.Models
{
    public partial class ApplicationStatus
    {
        public ApplicationStatus()
        {
            Application = new HashSet<Application>();
        }

        public int IdApplicationStatus { get; set; }
        public string Status { get; set; }

        public ICollection<Application> Application { get; set; }
    }
}
