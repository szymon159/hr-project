using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Project.Enums;
using HR_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HR_Project.Controllers
{
    public class JobOfferController : Controller
    {
        private static List<JobOfferViewModel> jobOffers = new List<JobOfferViewModel>
        {
            new JobOfferViewModel{Id = 1, JobTitle = "Backend Developer", Description = "Backend Developer with id = 1 and experience in creating bugs"},
            new JobOfferViewModel{Id = 2, JobTitle = "Frontend Developer", Description = "Frontend Developer with id = 2 and experience in creating requirements for nice-looking things which are unable to implement"},
            new JobOfferViewModel{Id = 3, JobTitle = "Manager", Description = "Manager with id = 3 and experience in managing things (doesn't really matter what kind of things)"},
            new JobOfferViewModel{Id = 4, JobTitle = "Teacher", Description = "Teacher with id = 4 who is ready to earn less than he should for his skills"},
            new JobOfferViewModel{Id = 5, JobTitle = "Cook", Description = "Finally some good funny person"}
        };

        public IActionResult Delete(int id)
        {
            var toDelete = jobOffers.Where(offer => offer.Id == id);
            if(toDelete.Count() == 1)
            {
                toDelete.First().Status = JobOfferStatus.Inactive;
            }

            return RedirectToAction("Index", "JobOffer");
        }

        public IActionResult Index()
        {
            return View(jobOffers);
        }

        public IActionResult Details(int id)
        {
            return View(jobOffers[id]);
        }
    }
}