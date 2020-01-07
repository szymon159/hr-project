using HR_Project_Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR_Project_Database.EntityFramework.Configuration
{
    public class ResponsibilityConfig : IEntityTypeConfiguration<Responsibility>
    {
        public void Configure(EntityTypeBuilder<Responsibility> builder)
        {
            builder.HasKey(e => e.IdResponsibility);

            builder.Property(e => e.IdResponsibility).HasColumnName("Id_Responsibility");

            builder.HasOne(d => d.JobOffer)
                .WithMany(p => p.Responsibility)
                .HasForeignKey(d => d.JobOfferId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Responsibility_JobOffer");

            builder.HasOne(d => d.User)
                .WithMany(p => p.Responsibility)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Responsibility_User");
        }
    }
}
