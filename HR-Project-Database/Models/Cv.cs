using System;
using System.Collections.Generic;

namespace HR_Project_Database.Models
{
    public partial class Cv
    {
        public Cv()
        {
            Application = new HashSet<Application>();
        }

        public int IdCv { get; set; }
        public string Cvpath { get; set; }

        public ICollection<Application> Application { get; set; }
    }
}
