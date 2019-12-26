using System;
using System.Collections.Generic;

namespace HR_Project_Database.Models
{
    public partial class Attachment
    {
        public int IdAttachment { get; set; }
        public string Extension { get; set; }
        public int? AttachmentGroupId { get; set; }

        public AttachmentGroup AttachmentGroup { get; set; }
    }
}
