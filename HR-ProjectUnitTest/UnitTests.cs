using HR_Project.DataLayer;
using HR_Project.ExtensionMethods;
using HR_Project.Notifications;
using HR_Project_Database.EntityFramework;
using HR_Project_Database.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace HR_ProjectTests
{
    public class UnitTests
    {
        JobOffer offer;
        User[] users;

        [SetUp]
        public void Setup()
        {
            var user1 = new User()
            {
                IdUser = 1,
                FirstName = "Test",
                LastName = "User",
                Email = "01130613@pw.edu.pl",
                Role = UserRole.HR,
                ExternalId = new Guid().ToString()
            };

            var user2 = user1;
            user2.FirstName = "OtherTest";
            user2.LastName = "Admin";

            users = new User[] { user1, user2 };

            var fakeOfferTitle = new Guid();
            offer = new JobOffer()
            {
                IdJobOffer = 1,
                JobTitle = fakeOfferTitle.ToString(),
                Responsibility = new Responsibility[]{
                    new Responsibility() { IdResponsibility = 1, User = user1, JobOfferId = 1},
                    new Responsibility() { IdResponsibility = 1, User = user2, JobOfferId = 1},
                },
                Description = "ABCD"
            };
        }

        [Test]
        public void NotificationHelper_ShouldSetSender()
        {
            var notification = NotificationHelper.CreateNotificationForOffer(offer);

            Assert.IsTrue(notification.From.Email == "01130613@pw.edu.pl");
            Assert.IsTrue(notification.From.Name == "HR Project");
        }

        [Test]
        public void NotificationHelper_ShouldSetReceipents()
        {
            var notification = NotificationHelper.CreateNotificationForOffer(offer);

            var receipentAddresses = notification.Personalizations.SelectMany(personalization => personalization.Tos).ToList();

            Assert.IsTrue(receipentAddresses.Count == users.Length);
            for(int i = 0; i < receipentAddresses.Count; i++)
            {
                var expectedName = string.Join(' ', users[i].FirstName, users[i].LastName);

                Assert.IsTrue(receipentAddresses[i].Email == users[i].Email);
                Assert.IsTrue(receipentAddresses[i].Name == expectedName);
            }
        }

        [Test]
        public void NotificationHelper_ShouldSetContent()
        {
            var notification = NotificationHelper.CreateNotificationForOffer(offer);

            foreach(var content in notification.Contents)
                Assert.IsFalse(string.IsNullOrWhiteSpace(content.Value));
            Assert.IsFalse(string.IsNullOrWhiteSpace(notification.Subject));

            foreach (var content in notification.Contents)
                Assert.IsTrue(content.Value.Contains(offer.JobTitle));
            Assert.IsTrue(notification.Subject.Contains(offer.JobTitle));
        }
    }
}