using HR_Project_Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR_Project_Database.EntityFramework.Configuration
{
    public class AttachmentGroupConfig : IEntityTypeConfiguration<AttachmentGroup>
    {
        public void Configure(EntityTypeBuilder<AttachmentGroup> builder)
        {
            builder.HasKey(e => e.IdAttachmentGroup);

            builder.Property(e => e.IdAttachmentGroup).HasColumnName("Id_AttachmentGroup");
        }
    }
}
