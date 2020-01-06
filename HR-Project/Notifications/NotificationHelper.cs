using HR_Project_Database.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Project.Notifications
{
    public static class NotificationHelper
    {
        private static readonly string SENDER_EMAIL = "01130613@pw.edu.pl";
        private static readonly string SENDER_NAME = "HR Project";
        private static readonly EmailAddress senderAddress = new EmailAddress(SENDER_EMAIL, SENDER_NAME);

        public static SendGridMessage CreateNotificationForOffer(JobOffer jobOffer)
        {
            var receipents = jobOffer.Responsibility.Select(responsibility => new EmailAddress(
                responsibility.User.Email,
                string.Join(' ', responsibility.User.FirstName, responsibility.User.LastName))).ToList();

            return CreateMessage(receipents, jobOffer.JobTitle);
        }

        private static SendGridMessage CreateMessage(List<EmailAddress> receipentAdresses, string jobOfferTitle)
        {
            (var plainText, var htmlContent, var subject) = GetNotificationContent(jobOfferTitle);

            var msg = MailHelper.CreateSingleEmailToMultipleRecipients
                (senderAddress, receipentAdresses, subject, plainText, htmlContent, false);

            return msg;
        }

        private static (string plainTextContent, string htmlContent, string subject) GetNotificationContent(string jobOfferTitle)
        {
            var plainText = string.Format("Hi, \n" +
                "New application has been submitted for following job offer:\n" +
                $"{jobOfferTitle}\n" +
                "Log into application to see it and approve (or reject).");

            var htmlContent = string.Format("<p>Hi,</p>" +
                "<p>New application has been submitted for following job offer:</p>" +
                $"<h2>{jobOfferTitle}</h2>\n" +
                "<p>Log into application to see it and approve (or reject).</p>");

            var subject = $"New application for {jobOfferTitle}";

            return (plainText, htmlContent, subject);
        }
    }
}
