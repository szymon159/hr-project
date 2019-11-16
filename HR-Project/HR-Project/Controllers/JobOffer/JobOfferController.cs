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
            new JobOfferViewModel{Id = 1, JobTitle = "BackendDeveloper"},
        };

        public IActionResult Index()
        {
            return View(jobOffers);
        }
    }
}