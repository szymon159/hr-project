using System;
using System.Collections.Generic;

namespace HR_ProjectDB.Models
{
    public partial class Responsibility
    {
        public int IdResponsibility { get; set; }
        public int UserId { get; set; }
        public int JobOfferId { get; set; }

        public JobOffer JobOffer { get; set; }
        public User User { get; set; }
    }
}
