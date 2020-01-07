using HR_Project_Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR_Project_Database.EntityFramework.Configuration
{
    public class ApplicationConfig : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.HasKey(e => e.IdApplication);

            builder.Property(e => e.IdApplication).HasColumnName("Id_Application");

            builder.Property(e => e.CvId).HasColumnName("CVId");

            builder.Property(e => e.Status).HasColumnName("Status");

            builder.HasOne(d => d.ApplicationMessage)
                .WithMany(p => p.Application)
                .HasForeignKey(d => d.ApplicationMessageId)
                .HasConstraintName("FK_Application_ApplicationMessage");

            builder.HasOne(d => d.AttachmentGroup)
                .WithMany(p => p.Application)
                .HasForeignKey(d => d.AttachmentGroupId)
                .HasConstraintName("FK_Application_AttachmentGroup");

            builder.HasOne(d => d.JobOffer)
                .WithMany(p => p.Application)
                .HasForeignKey(d => d.JobOfferId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_JobOffer");

            builder.HasOne(d => d.User)
                .WithMany(p => p.Application)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_User");
        }
    }
}
