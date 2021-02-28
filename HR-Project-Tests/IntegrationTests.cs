using HR_Project.DataLayer;
using HR_Project.ExtensionMethods;
using HR_Project_Database.EntityFramework;
using HR_Project_Database.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace HR_ProjectTests
{
    public class IntegrationTests
    {
        DataContext context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "Test").Options;
            context = new DataContext(options);
        }

        [Test]
        public void GetJobOffers_ShouldReturnAllOffersFromDB()
        {
            var toAdd = new JobOffer[]
            {
                new JobOffer{ IdJobOffer = 1, JobTitle = "Backend Developer", Description = "Backend Developer with id = 1 and experience in creating bugs", Status = JobOfferStatus.Active},
                new JobOffer{ IdJobOffer = 2, JobTitle = "Frontend Developer", Description = "Frontend Developer with id = 2 and experience in creating requirements for nice-looking things which are unable to implement", Status = JobOfferStatus.Active},
                new JobOffer{ IdJobOffer = 3, JobTitle = "Manager", Description = "Manager with id = 3 and experience in managing things (doesn't really matter what kind of things)", Status = JobOfferStatus.Active},
                new JobOffer{ IdJobOffer = 4, JobTitle = "Teacher", Description = "Teacher with id = 4 who is ready to earn less than he should for his skills", Status = JobOfferStatus.Active},
                new JobOffer{ IdJobOffer = 5, JobTitle = "Cook", Description = "Finally some good funny person", Status = JobOfferStatus.Active}
            };

            context.JobOffer.AddRange(toAdd);
            context.SaveChanges();

            var offers = DatabaseReader.GetJobOffers(context, true);

            Assert.AreEqual(offers.Count, 5);
            for(int i = 0; i < 5; i++)
            {
                Assert.IsTrue(offers[i].Id == toAdd[i].IdJobOffer
                    && offers[i].JobTitle == toAdd[i].JobTitle
                    && offers[i].Description == toAdd[i].Description
                    && offers[i].Status.ToString() == toAdd[i].Status.ToString()
                    && (int)offers[i].Status == (int)toAdd[i].Status);
            }

        }

        [Test, Ignore("Not in use")]
        public void AddUserIfNotExists_ShouldReturnIfUserExists()
        {
            var toAdd = new HR_Project.ViewModels.UserViewModel()
            {
                FirstName = "Test",
                LastName = "User",
                Email = "",
                Role = HR_Project.Enums.UserRole.User,
                ExternalId = new Guid().ToString()
            };

            var users = context.User.ToList();
            context.User.RemoveRange(users);
            Assert.IsTrue(context.AddUserIfNotExists(toAdd, out _));
            Assert.IsFalse(context.AddUserIfNotExists(toAdd, out _));
        }

        [Test]
        public void AddUserIfNotExists_ShouldAddCorrectUser()
        {
            var toAdd = new HR_Project.ViewModels.UserViewModel()
            {
                FirstName = "Test",
                LastName = "User",
                Email = "",
                Role = HR_Project.Enums.UserRole.HR,
                ExternalId = new Guid().ToString()
            };

            Assert.IsTrue(context.AddUserIfNotExists(toAdd, out var userRole));

            var addedUser = context.User
                .FirstOrDefault(user => user.ExternalId == toAdd.ExternalId);

            Assert.IsTrue(toAdd.FirstName == addedUser.FirstName
                && toAdd.LastName == addedUser.LastName
                && toAdd.Email == addedUser.Email
                && toAdd.Role.ToString() == addedUser.Role.ToString()
                && (int)toAdd.Role == (int)addedUser.Role);
        }
    }
}