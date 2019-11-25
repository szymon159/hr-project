using System;
using System.Collections.Generic;

namespace HR_ProjectDB.Models
{
    public partial class Application
    {
        public int IdApplication { get; set; }
        public int JobOfferId { get; set; }
        public int UserId { get; set; }
        public int Cvid { get; set; }
        public int? AttachmentGroupId { get; set; }
        public int? ApplicationMessageId { get; set; }
        public int ApplicationStatusId { get; set; }

        public ApplicationMessage ApplicationMessage { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
        public AttachmentGroup AttachmentGroup { get; set; }
        public Cv Cv { get; set; }
        public JobOffer JobOffer { get; set; }
        public User User { get; set; }
    }
}
