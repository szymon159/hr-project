using System;
using HR_Project_Database.EntityFramework.Configuration;
using HR_Project_Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HR_Project_Database.EntityFramework
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<ApplicationMessage> ApplicationMessage { get; set; }
        public virtual DbSet<Attachment> Attachment { get; set; }
        public virtual DbSet<AttachmentGroup> AttachmentGroup { get; set; }
        public virtual DbSet<JobOffer> JobOffer { get; set; }
        public virtual DbSet<Responsibility> Responsibility { get; set; }
        public virtual DbSet<User> User { get; set; }

        // TODO: Update this method to use data making sense :)
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
                new User{FirstName="Adam", LastName="Małysz", Email="a", Role=UserRole.User, ExternalId="1"}
            };
            User.AddRange(users);
            SaveChanges();

            var applications = new Application[]
            {
                new Application{JobOfferId = 1, UserId = 1, CvId = null, Status=ApplicationStatus.Approved},
                new Application{JobOfferId = 2, UserId = 1, CvId = null, Status=ApplicationStatus.Rejected},
                new Application{JobOfferId = 3, UserId = 1, CvId = null, Status=ApplicationStatus.Submitted},
                new Application{JobOfferId = 4, UserId = 1, CvId = null, Status=ApplicationStatus.Submitted},
                new Application{JobOfferId = 5, UserId = 1, CvId = null, Status=ApplicationStatus.Submitted},
            };
            Application.AddRange(applications);
            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new JobOfferConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new AttachmentGroupConfig());
            modelBuilder.ApplyConfiguration(new AttachmentConfig());
            modelBuilder.ApplyConfiguration(new ApplicationConfig());
            modelBuilder.ApplyConfiguration(new ApplicationMessageConfig());
            modelBuilder.ApplyConfiguration(new ResponsibilityConfig());
        }
    }
}
