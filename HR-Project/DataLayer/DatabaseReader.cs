using HR_Project.ViewModels;
using HR_Project_Database.EntityFramework;
using HR_Project.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Project.DataLayer
{
    public static class DatabaseReader
    {
        public static List<JobOfferViewModel> GetJobOffers(DataContext context, bool includeDetails = false)
        {
            var result = new List<JobOfferViewModel>();
            foreach(var joboffer in context.JobOffer)
            {
                result.Add(new JobOfferViewModel
                {
                    Id = joboffer.IdJobOffer,
                    JobTitle = joboffer.JobTitle,
                    Description = includeDetails ? joboffer.Description : null,
                    Status = (JobOfferStatus)Enum.Parse(typeof(JobOfferStatus), joboffer.Status.ToString())
                });
            }

            return result;
        }

        public static void GetJobOfferDetails(this JobOfferViewModel model, DataContext context)
        {
            model.Description = context.JobOffer.Where(jobOffer => jobOffer.IdJobOffer == model.Id).FirstOrDefault().Description;
        }

        public static List<ApplicationViewModel> GetApplications(DataContext context, bool includeDetails = false)
        {
            var result = new List<ApplicationViewModel>();
            foreach(var application in context.Application)
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

        public static void GetApplicationDetails(this ApplicationViewModel model, DataContext context)
        {
            var application = context.Application.Find(model.Id);
            var user = context.User.Find(application.UserId);
            var attachments = context.Attachment.Where(attachment => attachment.AttachmentGroupId == application.AttachmentGroupId);

            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.CV = context.Cv.Find(application.Cvid)?.Cvpath;
            model.OtherAttachments = attachments.Select(attachment => attachment.AttachmentPath)?.ToArray();
            model.Message = context.ApplicationMessage.Find(application.ApplicationMessageId)?.MessageContent;
        }
    }
}
