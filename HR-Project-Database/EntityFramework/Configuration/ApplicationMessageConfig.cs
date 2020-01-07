using HR_Project_Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR_Project_Database.EntityFramework.Configuration
{
    public class ApplicationMessageConfig : IEntityTypeConfiguration<ApplicationMessage>
    {
        public void Configure(EntityTypeBuilder<ApplicationMessage> builder)
        {
            builder.HasKey(e => e.IdApplicationMessage);

            builder.Property(e => e.IdApplicationMessage).HasColumnName("Id_ApplicationMessage");

            builder.Property(e => e.MessageContent)
                .IsRequired()
                .HasColumnType("text");
        }
    }
}
