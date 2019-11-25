using System;
using System.Collections.Generic;

namespace HR_ProjectDB.Models
{
    public partial class JobOfferStatus
    {
        public JobOfferStatus()
        {
            JobOffer = new HashSet<JobOffer>();
        }

        public int IdJobOfferStatus { get; set; }
        public string Status { get; set; }

        public ICollection<JobOffer> JobOffer { get; set; }
    }
}
