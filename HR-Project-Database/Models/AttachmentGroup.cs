using System;
using System.Collections.Generic;

namespace HR_Project_Database.Models
{
    public partial class AttachmentGroup
    {
        public AttachmentGroup()
        {
            Application = new HashSet<Application>();
            Attachment = new HashSet<Attachment>();
            JobOffer = new HashSet<JobOffer>();
        }

        public int IdAttachmentGroup { get; set; }

        public ICollection<Application> Application { get; set; }
        public ICollection<Attachment> Attachment { get; set; }
        public ICollection<JobOffer> JobOffer { get; set; }
    }
}
