using System;
using System.Collections.Generic;

namespace HR_ProjectDB.Models
{
    public partial class Attachment
    {
        public int IdAttachment { get; set; }
        public string AttachmentPath { get; set; }
        public int? AttachmentGroupId { get; set; }

        public AttachmentGroup AttachmentGroup { get; set; }
    }
}
