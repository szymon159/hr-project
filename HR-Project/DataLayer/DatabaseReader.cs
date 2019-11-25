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
        public static List<JobOfferViewModel> GetJobOffers(DataContext context, bool includeDescription = false)
        {
            var result = new List<JobOfferViewModel>();
            Parallel.ForEach(context.JobOffer, joboffer =>
            {
                result.Add(new JobOfferViewModel
                {
                    Id = joboffer.IdJobOffer,
                    JobTitle = joboffer.JobTitle,
                    Description = includeDescription ? joboffer.Description : null,
                    Status = (JobOfferStatus)Enum.Parse(typeof(JobOfferStatus), joboffer.Status.ToString())
                });
            });

            return result;
        }

        public static string GetJobOfferDescription(DataContext context, int jobOfferId)
        {
            return context.JobOffer.Where(jobOffer => jobOffer.IdJobOffer == jobOfferId).FirstOrDefault().Description;
        }
    }
}
