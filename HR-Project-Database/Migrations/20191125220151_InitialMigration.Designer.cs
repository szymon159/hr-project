﻿// <auto-generated />
using System;
using HR_Project_Database.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HR_Project_Database.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20191125220151_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HR_Project_Database.Models.Application", b =>
                {
                    b.Property<int>("IdApplication")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id_Application")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ApplicationMessageId");

                    b.Property<int?>("AttachmentGroupId");

                    b.Property<int>("Cvid")
                        .HasColumnName("CVId");

                    b.Property<int>("JobOfferId");

                    b.Property<int>("Status")
                        .HasColumnName("Status");

                    b.Property<int>("UserId");

                    b.HasKey("IdApplication");

                    b.HasIndex("ApplicationMessageId");

                    b.HasIndex("AttachmentGroupId");

                    b.HasIndex("Cvid");

                    b.HasIndex("JobOfferId");

                    b.HasIndex("UserId");

                    b.ToTable("Application");
                });

            modelBuilder.Entity("HR_Project_Database.Models.ApplicationMessage", b =>
                {
                    b.Property<int>("IdApplicationMessage")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id_ApplicationMessage")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MessageContent")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdApplicationMessage");

                    b.ToTable("ApplicationMessage");
                });

            modelBuilder.Entity("HR_Project_Database.Models.Attachment", b =>
                {
                    b.Property<int>("IdAttachment")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id_Attachment")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AttachmentGroupId");

                    b.Property<string>("AttachmentPath")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("IdAttachment");

                    b.HasIndex("AttachmentGroupId");

                    b.ToTable("Attachment");
                });

            modelBuilder.Entity("HR_Project_Database.Models.AttachmentGroup", b =>
                {
                    b.Property<int>("IdAttachmentGroup")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id_AttachmentGroup")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("IdAttachmentGroup");

                    b.ToTable("AttachmentGroup");
                });

            modelBuilder.Entity("HR_Project_Database.Models.Cv", b =>
                {
                    b.Property<int>("IdCv")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id_CV")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cvpath")
                        .IsRequired()
                        .HasColumnName("CVPath")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("IdCv");

                    b.ToTable("CV");
                });

            modelBuilder.Entity("HR_Project_Database.Models.JobOffer", b =>
                {
                    b.Property<int>("IdJobOffer")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id_JobOffer")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AttachmentGroupId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("Status")
                        .HasColumnName("Status");

                    b.HasKey("IdJobOffer");

                    b.HasIndex("AttachmentGroupId");

                    b.ToTable("JobOffer");
                });

            modelBuilder.Entity("HR_Project_Database.Models.Responsibility", b =>
                {
                    b.Property<int>("IdResponsibility")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id_Responsibility")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("JobOfferId");

                    b.Property<int>("UserId");

                    b.HasKey("IdResponsibility");

                    b.HasIndex("JobOfferId");

                    b.HasIndex("UserId");

                    b.ToTable("Responsibility");
                });

            modelBuilder.Entity("HR_Project_Database.Models.User", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id_User")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("Role")
                        .HasColumnName("Role");

                    b.Property<int>("UserProfileId");

                    b.HasKey("IdUser");

                    b.ToTable("User");
                });

            modelBuilder.Entity("HR_Project_Database.Models.Application", b =>
                {
                    b.HasOne("HR_Project_Database.Models.ApplicationMessage", "ApplicationMessage")
                        .WithMany("Application")
                        .HasForeignKey("ApplicationMessageId")
                        .HasConstraintName("FK_Application_ApplicationMessage");

                    b.HasOne("HR_Project_Database.Models.AttachmentGroup", "AttachmentGroup")
                        .WithMany("Application")
                        .HasForeignKey("AttachmentGroupId")
                        .HasConstraintName("FK_Application_AttachmentGroup");

                    b.HasOne("HR_Project_Database.Models.Cv", "Cv")
                        .WithMany("Application")
                        .HasForeignKey("Cvid")
                        .HasConstraintName("FK_Application_CV");

                    b.HasOne("HR_Project_Database.Models.JobOffer", "JobOffer")
                        .WithMany("Application")
                        .HasForeignKey("JobOfferId")
                        .HasConstraintName("FK_Application_JobOffer");

                    b.HasOne("HR_Project_Database.Models.User", "User")
                        .WithMany("Application")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Application_User");
                });

            modelBuilder.Entity("HR_Project_Database.Models.Attachment", b =>
                {
                    b.HasOne("HR_Project_Database.Models.AttachmentGroup", "AttachmentGroup")
                        .WithMany("Attachment")
                        .HasForeignKey("AttachmentGroupId")
                        .HasConstraintName("FK_Attachment_AttachmentGroup");
                });

            modelBuilder.Entity("HR_Project_Database.Models.JobOffer", b =>
                {
                    b.HasOne("HR_Project_Database.Models.AttachmentGroup", "AttachmentGroup")
                        .WithMany("JobOffer")
                        .HasForeignKey("AttachmentGroupId")
                        .HasConstraintName("FK_JobOffer_AttachmentGroup");
                });

            modelBuilder.Entity("HR_Project_Database.Models.Responsibility", b =>
                {
                    b.HasOne("HR_Project_Database.Models.JobOffer", "JobOffer")
                        .WithMany("Responsibility")
                        .HasForeignKey("JobOfferId")
                        .HasConstraintName("FK_Responsibility_JobOffer");

                    b.HasOne("HR_Project_Database.Models.User", "User")
                        .WithMany("Responsibility")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Responsibility_User");
                });
#pragma warning restore 612, 618
        }
    }
}
