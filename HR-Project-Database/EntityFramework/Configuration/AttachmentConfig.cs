using HR_Project_Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR_Project_Database.EntityFramework.Configuration
{
    public class AttachmentConfig : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.HasKey(e => e.IdAttachment);

            builder.Property(e => e.IdAttachment).HasColumnName("Id_Attachment");

            builder.Property(e => e.Extension)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(true);

            builder.HasOne(d => d.AttachmentGroup)
                .WithMany(p => p.Attachment)
                .HasForeignKey(d => d.AttachmentGroupId)
                .HasConstraintName("FK_Attachment_AttachmentGroup");

            builder.HasData(new Attachment[]
            {
                new Attachment(){ IdAttachment = 1, Extension=".pdf", AttachmentGroupId = 1},
                new Attachment(){ IdAttachment = 2, Extension=".pdf", AttachmentGroupId = 1}
            });
        }
    }
}
