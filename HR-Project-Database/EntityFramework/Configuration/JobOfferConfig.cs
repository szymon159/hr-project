using HR_Project_Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR_Project_Database.EntityFramework.Configuration
{
    public class JobOfferConfig : IEntityTypeConfiguration<JobOffer>
    {
        public void Configure(EntityTypeBuilder<JobOffer> builder)
        {
            builder.HasKey(e => e.IdJobOffer);

            builder.Property(e => e.IdJobOffer).HasColumnName("Id_JobOffer");

            builder.Property(e => e.Status).HasColumnName("Status");

            builder.Property(e => e.Description)
                .IsRequired()
                .HasColumnType("text");

            builder.Property(e => e.JobTitle)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.HasOne(d => d.AttachmentGroup)
                .WithMany(p => p.JobOffer)
                .HasForeignKey(d => d.AttachmentGroupId)
                .HasConstraintName("FK_JobOffer_AttachmentGroup");
        }
    }
}
