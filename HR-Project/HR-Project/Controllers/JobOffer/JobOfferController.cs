using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HR_Project.Controllers
{
    public class JobOfferController : Controller
    {
        private static List<JobOfferViewModel> jobOffers = new List<JobOfferViewModel>
        {
            new JobOfferViewModel{Id = 1, JobTitle = "Backend Developer"},
            new JobOfferViewModel{Id = 2, JobTitle = "Frontend Developer"},
            new JobOfferViewModel{Id = 3, JobTitle = "Manager"},
            new JobOfferViewModel{Id = 4, JobTitle = "Teacher"},
            new JobOfferViewModel{Id = 5, JobTitle = "Cook"}
        };

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