using System;
using System.Collections.Generic;

namespace HR_ProjectDB.Models
{
    public partial class ApplicationMessage
    {
        public ApplicationMessage()
        {
            Application = new HashSet<Application>();
        }

        public int IdApplicationMessage { get; set; }
        public string MessageContent { get; set; }

        public ICollection<Application> Application { get; set; }
    }
}
