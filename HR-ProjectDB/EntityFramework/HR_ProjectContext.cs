using System;
using HR_ProjectDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HR_ProjectDB.EntityFramework
{
    public partial class HR_ProjectContext : DbContext
    {
        public HR_ProjectContext()
        {
        }

        public HR_ProjectContext(DbContextOptions<HR_ProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<ApplicationMessage> ApplicationMessage { get; set; }
        public virtual DbSet<ApplicationStatus> ApplicationStatus { get; set; }
        public virtual DbSet<Attachment> Attachment { get; set; }
        public virtual DbSet<AttachmentGroup> AttachmentGroup { get; set; }
        public virtual DbSet<Cv> Cv { get; set; }
        public virtual DbSet<JobOffer> JobOffer { get; set; }
        public virtual DbSet<JobOfferStatus> JobOfferStatus { get; set; }
        public virtual DbSet<Responsibility> Responsibility { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer(Configuration["DatabaseConnectionString"]);
                throw new ApplicationException("Trying to connect to DB with undefined SQL Server connection string");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasKey(e => e.IdApplication);

                entity.Property(e => e.IdApplication).HasColumnName("Id_Application");

                entity.Property(e => e.Cvid).HasColumnName("CVId");

                entity.HasOne(d => d.ApplicationMessage)
                    .WithMany(p => p.Application)
                    .HasForeignKey(d => d.ApplicationMessageId)
                    .HasConstraintName("FK_Application_ApplicationMessage");

                entity.HasOne(d => d.ApplicationStatus)
                    .WithMany(p => p.Application)
                    .HasForeignKey(d => d.ApplicationStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Application_ApplicationStatus");

                entity.HasOne(d => d.AttachmentGroup)
                    .WithMany(p => p.Application)
                    .HasForeignKey(d => d.AttachmentGroupId)
                    .HasConstraintName("FK_Application_AttachmentGroup");

                entity.HasOne(d => d.Cv)
                    .WithMany(p => p.Application)
                    .HasForeignKey(d => d.Cvid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Application_CV");

                entity.HasOne(d => d.JobOffer)
                    .WithMany(p => p.Application)
                    .HasForeignKey(d => d.JobOfferId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Application_JobOffer");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Application)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Application_User");
            });

            modelBuilder.Entity<ApplicationMessage>(entity =>
            {
                entity.HasKey(e => e.IdApplicationMessage);

                entity.Property(e => e.IdApplicationMessage).HasColumnName("Id_ApplicationMessage");

                entity.Property(e => e.MessageContent)
                    .IsRequired()
                    .HasColumnType("text");
            });

            modelBuilder.Entity<ApplicationStatus>(entity =>
            {
                entity.HasKey(e => e.IdApplicationStatus);

                entity.Property(e => e.IdApplicationStatus).HasColumnName("Id_ApplicationStatus");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.HasKey(e => e.IdAttachment);

                entity.Property(e => e.IdAttachment).HasColumnName("Id_Attachment");

                entity.Property(e => e.AttachmentPath)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.AttachmentGroup)
                    .WithMany(p => p.Attachment)
                    .HasForeignKey(d => d.AttachmentGroupId)
                    .HasConstraintName("FK_Attachment_AttachmentGroup");
            });

            modelBuilder.Entity<AttachmentGroup>(entity =>
            {
                entity.HasKey(e => e.IdAttachmentGroup);

                entity.Property(e => e.IdAttachmentGroup).HasColumnName("Id_AttachmentGroup");
            });

            modelBuilder.Entity<Cv>(entity =>
            {
                entity.HasKey(e => e.IdCv);

                entity.ToTable("CV");

                entity.Property(e => e.IdCv).HasColumnName("Id_CV");

                entity.Property(e => e.Cvpath)
                    .IsRequired()
                    .HasColumnName("CVPath")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JobOffer>(entity =>
            {
                entity.HasKey(e => e.IdJobOffer);

                entity.Property(e => e.IdJobOffer).HasColumnName("Id_JobOffer");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.JobTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.AttachmentGroup)
                    .WithMany(p => p.JobOffer)
                    .HasForeignKey(d => d.AttachmentGroupId)
                    .HasConstraintName("FK_JobOffer_AttachmentGroup");

                entity.HasOne(d => d.JobOfferStatus)
                    .WithMany(p => p.JobOffer)
                    .HasForeignKey(d => d.JobOfferStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobOffer_JobOfferStatus");
            });

            modelBuilder.Entity<JobOfferStatus>(entity =>
            {
                entity.HasKey(e => e.IdJobOfferStatus);

                entity.Property(e => e.IdJobOfferStatus).HasColumnName("Id_JobOfferStatus");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Responsibility>(entity =>
            {
                entity.HasKey(e => e.IdResponsibility);

                entity.Property(e => e.IdResponsibility).HasColumnName("Id_Responsibility");

                entity.HasOne(d => d.JobOffer)
                    .WithMany(p => p.Responsibility)
                    .HasForeignKey(d => d.JobOfferId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Responsibility_JobOffer");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Responsibility)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Responsibility_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.Property(e => e.IdUser).HasColumnName("Id_User");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserRole");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.IdUserRole);

                entity.Property(e => e.IdUserRole).HasColumnName("Id_UserRole");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });
        }
    }
}
