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

            builder.HasData(new JobOffer[]
            {
                new JobOffer{ IdJobOffer = 1, JobTitle = "Backend Developer", Description = "Backend Developer with id = 1 and experience in creating bugs", Status = JobOfferStatus.Active},
                new JobOffer{ IdJobOffer = 2, JobTitle = "Frontend Developer", Description = "Frontend Developer with id = 2 and experience in creating requirements for nice-looking things which are unable to implement", Status = JobOfferStatus.Active},
                new JobOffer{ IdJobOffer = 3, JobTitle = "Manager", Description = "Manager with id = 3 and experience in managing things (doesn't really matter what kind of things)", Status = JobOfferStatus.Active},
                new JobOffer{ IdJobOffer = 4, JobTitle = "Teacher", Description = "Teacher with id = 4 who is ready to earn less than he should for his skills", Status = JobOfferStatus.Active},
                new JobOffer{ IdJobOffer = 5, JobTitle = "Cook", Description = "Finally some good funny person", Status = JobOfferStatus.Active}
            });
        }
    }
}
