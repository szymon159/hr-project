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
        public virtual DbSet<Attachment> Attachment { get; set; }
        public virtual DbSet<AttachmentGroup> AttachmentGroup { get; set; }
        public virtual DbSet<Cv> Cv { get; set; }
        public virtual DbSet<JobOffer> JobOffer { get; set; }
        public virtual DbSet<Responsibility> Responsibility { get; set; }
        public virtual DbSet<User> User { get; set; }

        public void InitializeFakeData()
        {
            var jobOffers = new JobOffer[]
            {
                new JobOffer{JobTitle = "Backend Developer", Description = "Backend Developer with id = 1 and experience in creating bugs", Status = JobOfferStatus.Active},
                new JobOffer{JobTitle = "Frontend Developer", Description = "Frontend Developer with id = 2 and experience in creating requirements for nice-looking things which are unable to implement", Status = JobOfferStatus.Active},
                new JobOffer{JobTitle = "Manager", Description = "Manager with id = 3 and experience in managing things (doesn't really matter what kind of things)", Status = JobOfferStatus.Active},
                new JobOffer{JobTitle = "Teacher", Description = "Teacher with id = 4 who is ready to earn less than he should for his skills", Status = JobOfferStatus.Active},
                new JobOffer{ JobTitle = "Cook", Description = "Finally some good funny person", Status = JobOfferStatus.Active}
            };
            JobOffer.AddRange(jobOffers);
            SaveChanges();

            var users = new User[]
            {
                new User{FirstName="Adam", LastName="Małysz", Email="a", Role=UserRole.User, UserProfileId=1}
            };
            User.AddRange(users);
            SaveChanges();

            var Cvs = new Cv[]
            {
                new Cv{Cvpath = "TestCV"}
            };
            Cv.AddRange(Cvs);
            SaveChanges();

            var applications = new Application[]
            {
                new Application{JobOfferId = 1, UserId = 1, Cvid = 1, Status=ApplicationStatus.Approved},
                new Application{JobOfferId = 2, UserId = 1, Cvid = 1, Status=ApplicationStatus.Rejected},
                new Application{JobOfferId = 3, UserId = 1, Cvid = 1, Status=ApplicationStatus.Submitted},
                new Application{JobOfferId = 4, UserId = 1, Cvid = 1, Status=ApplicationStatus.Submitted},
                new Application{JobOfferId = 5, UserId = 1, Cvid = 1, Status=ApplicationStatus.Submitted},
            };
            Application.AddRange(applications);
            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasKey(e => e.IdApplication);

                entity.Property(e => e.IdApplication).HasColumnName("Id_Application");

                entity.Property(e => e.Cvid).HasColumnName("CVId");

                entity.Property(e => e.Status).HasColumnName("Status");

                entity.HasOne(d => d.ApplicationMessage)
                    .WithMany(p => p.Application)
                    .HasForeignKey(d => d.ApplicationMessageId)
                    .HasConstraintName("FK_Application_ApplicationMessage");

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

                entity.Property(e => e.Status).HasColumnName("Status");

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

                entity.Property(e => e.Role).HasColumnName("Role");

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
            });
        }
    }
}
