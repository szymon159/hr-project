using System;
using System.Collections.Generic;

namespace HR_ProjectDB.Models
{
    public partial class JobOffer
    {
        public JobOffer()
        {
            Application = new HashSet<Application>();
            Responsibility = new HashSet<Responsibility>();
        }

        public int IdJobOffer { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public JobOfferStatus Status { get; set; }
        public int? AttachmentGroupId { get; set; }

        public AttachmentGroup AttachmentGroup { get; set; }
        public ICollection<Application> Application { get; set; }
        public ICollection<Responsibility> Responsibility { get; set; }
    }
}
