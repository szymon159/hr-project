﻿using HR_Project_Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR_Project_Database.EntityFramework.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.IdUser);

            builder.Property(e => e.IdUser).HasColumnName("Id_User");

            builder.Property(e => e.Role).HasColumnName("Role");

            builder.Property(e => e.ExternalId)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.HasIndex(e => e.ExternalId);

            builder.HasData(new User[]
            {
                new User{ IdUser = 1, FirstName="Adam", LastName="Małysz", Email="a", Role=UserRole.User, ExternalId="1"},
                new User{ IdUser = 2, FirstName="Dawid", LastName="Kubacki", Email="s.stasiak@student.mini.pw.edu.pl", Role=UserRole.HR, ExternalId="2"}
            });
        }
    }
}
