using HR_Project.ViewModels;
using HR_Project_Database.EntityFramework;
using HR_Project.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Internal;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HR_Project.DataLayer
{
    public static class DatabaseReader
    {
        public static List<JobOfferViewModel> GetJobOffers(DataContext context, bool includeDetails = false)
        {
            var result = new List<JobOfferViewModel>();
            foreach (var joboffer in context.JobOffer.Include(x => x.Responsibility).ThenInclude(x => x.User))
            {
                result.Add(new JobOfferViewModel
                {
                    Id = joboffer.IdJobOffer,
                    JobTitle = joboffer.JobTitle,
                    Description = includeDetails ? joboffer.Description : null,
                    ResponsibleExternalIds = joboffer.Responsibility.Select(x => x.User.ExternalId).ToList(),
                    Status = (JobOfferStatus)Enum.Parse(typeof(JobOfferStatus), joboffer.Status.ToString())
                });
            }

            return result;
        }

        public static void GetJobOfferDetails(this JobOfferViewModel model, DataContext context)
        {
            model.Description = context.JobOffer.Where(jobOffer => jobOffer.IdJobOffer == model.Id).FirstOrDefault().Description;
        }

        public static List<ApplicationViewModel> GetApplications(DataContext context, string userExternalId = null, string userRole = null, bool includeDetails = false)
        {
            var result = new List<ApplicationViewModel>();
            var applications = GetApplicationsForUser(context, userExternalId, userRole);

            foreach(var application in applications)
            {
                var toAdd = new ApplicationViewModel
                {
                    Id = application.IdApplication,
                    JobTitle = context.JobOffer.Find(application.JobOfferId).JobTitle,
                    Status = (ApplicationStatus)Enum.Parse(typeof(ApplicationStatus), application.Status.ToString())
                };

                if(includeDetails)
                {
                    toAdd.GetApplicationDetails(context);
                }
                result.Add(toAdd);
            }

            return result;
        }

        private static IEnumerable<HR_Project_Database.Models.Application> GetApplicationsForUser(DataContext context, string userExternalId, string userRole)
        {
            if (userRole == UserRole.User.ToString())
            {
                return context.Application
                    .Where(application => application.User.ExternalId == userExternalId);
            }
            else if (userRole == UserRole.HR.ToString())
            {
                // Select Applications for JobOffers which are managed by user with requested userExternalId
                return context.Responsibility.Where(responsibility => responsibility.User.ExternalId == userExternalId)
                    .SelectMany(responsibility => responsibility.JobOffer.Application)
                    .Where(application => application.Status != HR_Project_Database.Models.ApplicationStatus.Draft);
            }
            else
            {
                return context.Application
                    .Where(application => application.Status != HR_Project_Database.Models.ApplicationStatus.Draft);
            }
        }

        public static void GetApplicationDetails(this ApplicationViewModel model, DataContext context)
        {
            var application = context.Application.Include(x => x.User).FirstOrDefault(app => app.IdApplication == model.Id);
            var attachments = context.Attachment.Where(attachment => attachment.AttachmentGroupId == application.AttachmentGroupId)
                .Select(attachment => attachment.IdAttachment.ToString() + attachment.Extension);

            model.FirstName = application.User.FirstName;
            model.LastName = application.User.LastName;
            model.Email = application.User.Email;
            model.UploadedCvPath = application.Cvid.ToString() + ".pdf";
            model.UploadedAttachmentPaths = attachments.ToList();
            model.Message = context.ApplicationMessage.Find(application.ApplicationMessageId)?.MessageContent;
        }
    }
}
