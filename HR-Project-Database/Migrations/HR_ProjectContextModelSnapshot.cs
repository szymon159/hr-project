﻿// <auto-generated />
using System;
using HR_Project_Database.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HR_Project_Database.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class HR_ProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<Guid?>("CvId")
                        .HasColumnName("CVId");

                    b.Property<int>("JobOfferId");

                    b.Property<int>("Status")
                        .HasColumnName("Status");

                    b.Property<int>("UserId");

                    b.HasKey("IdApplication");

                    b.HasIndex("ApplicationMessageId");

                    b.HasIndex("AttachmentGroupId");

                    b.HasIndex("JobOfferId");

                    b.HasIndex("UserId");

                    b.ToTable("Application");

                    b.HasData(
                        new { IdApplication = 1, ApplicationMessageId = 1, AttachmentGroupId = 1, CvId = new Guid("3b8cbf16-a004-45ef-bfb4-7364a3c67945"), JobOfferId = 1, Status = 2, UserId = 1 },
                        new { IdApplication = 2, CvId = new Guid("b35d7728-fb45-47cd-9794-bd54ea417bb4"), JobOfferId = 2, Status = 3, UserId = 1 },
                        new { IdApplication = 3, CvId = new Guid("3b8cbf16-a004-45ef-bfb4-7364a3c67945"), JobOfferId = 3, Status = 1, UserId = 1 },
                        new { IdApplication = 4, AttachmentGroupId = 1, CvId = new Guid("b35d7728-fb45-47cd-9794-bd54ea417bb4"), JobOfferId = 4, Status = 1, UserId = 1 },
                        new { IdApplication = 5, AttachmentGroupId = 1, CvId = new Guid("cfabe3bc-66b7-49e5-9bf4-be0e19520140"), JobOfferId = 5, Status = 1, UserId = 1 }
                    );
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

                    b.HasData(
                        new { IdApplicationMessage = 1, MessageContent = "Test message content" }
                    );
                });

            modelBuilder.Entity("HR_Project_Database.Models.Attachment", b =>
                {
                    b.Property<int>("IdAttachment")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id_Attachment")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AttachmentGroupId");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(true);

                    b.HasKey("IdAttachment");

                    b.HasIndex("AttachmentGroupId");

                    b.ToTable("Attachment");

                    b.HasData(
                        new { IdAttachment = 1, AttachmentGroupId = 1, Extension = ".pdf" },
                        new { IdAttachment = 2, AttachmentGroupId = 1, Extension = ".pdf" }
                    );
                });

            modelBuilder.Entity("HR_Project_Database.Models.AttachmentGroup", b =>
                {
                    b.Property<int>("IdAttachmentGroup")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id_AttachmentGroup")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("IdAttachmentGroup");

                    b.ToTable("AttachmentGroup");

                    b.HasData(
                        new { IdAttachmentGroup = 1 }
                    );
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

                    b.HasData(
                        new { IdJobOffer = 1, Description = "Backend Developer with id = 1 and experience in creating bugs", JobTitle = "Backend Developer", Status = 0 },
                        new { IdJobOffer = 2, Description = "Frontend Developer with id = 2 and experience in creating requirements for nice-looking things which are unable to implement", JobTitle = "Frontend Developer", Status = 0 },
                        new { IdJobOffer = 3, Description = "Manager with id = 3 and experience in managing things (doesn't really matter what kind of things)", JobTitle = "Manager", Status = 0 },
                        new { IdJobOffer = 4, Description = "Teacher with id = 4 who is ready to earn less than he should for his skills", JobTitle = "Teacher", Status = 0 },
                        new { IdJobOffer = 5, Description = "Finally some good funny person", JobTitle = "Cook", Status = 0 }
                    );
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

                    b.HasData(
                        new { IdResponsibility = 1, JobOfferId = 2, UserId = 2 },
                        new { IdResponsibility = 2, JobOfferId = 5, UserId = 2 }
                    );
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

                    b.Property<string>("ExternalId")
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

                    b.HasKey("IdUser");

                    b.HasIndex("ExternalId");

                    b.ToTable("User");

                    b.HasData(
                        new { IdUser = 1, Email = "a", ExternalId = "1", FirstName = "Adam", LastName = "Małysz", Role = 1 },
                        new { IdUser = 2, Email = "s.stasiak@student.mini.pw.edu.pl", ExternalId = "2", FirstName = "Dawid", LastName = "Kubacki", Role = 2 }
                    );
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
