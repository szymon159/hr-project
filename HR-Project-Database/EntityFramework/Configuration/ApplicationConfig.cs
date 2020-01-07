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

            builder.HasData(new Application[]
            {
                new Application{ IdApplication = 1, JobOfferId = 1, UserId = 1, CvId = new Guid("3b8cbf16-a004-45ef-bfb4-7364a3c67945"), AttachmentGroupId=1, ApplicationMessageId = 1, Status=ApplicationStatus.Approved},
                new Application{ IdApplication = 2, JobOfferId = 2, UserId = 1, CvId = new Guid("b35d7728-fb45-47cd-9794-bd54ea417bb4"), Status=ApplicationStatus.Rejected},
                new Application{ IdApplication = 3, JobOfferId = 3, UserId = 1, CvId = new Guid("3b8cbf16-a004-45ef-bfb4-7364a3c67945"), Status=ApplicationStatus.Submitted},
                new Application{ IdApplication = 4, JobOfferId = 4, UserId = 1, CvId = new Guid("b35d7728-fb45-47cd-9794-bd54ea417bb4"), AttachmentGroupId=1, Status=ApplicationStatus.Submitted},
                new Application{ IdApplication = 5, JobOfferId = 5, UserId = 1, CvId = new Guid("cfabe3bc-66b7-49e5-9bf4-be0e19520140"), AttachmentGroupId=1, Status=ApplicationStatus.Submitted},
            });
        }
    }
}
