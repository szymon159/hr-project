using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Project.Enums;
using HR_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HR_Project.Controllers.Application
{
    public class ApplicationController : Controller
    {
         private static List<ApplicationViewModel> applications = new List<ApplicationViewModel>
         { 
            new ApplicationViewModel{Id = 1, JobTitle = "Backend Developer", Status=ApplicationStatus.Approved},
            new ApplicationViewModel{Id = 2, JobTitle = "Frontend Developer", Status=ApplicationStatus.Rejected},
            new ApplicationViewModel{Id = 3, JobTitle = "Manager", Status=ApplicationStatus.Submitted},
            new ApplicationViewModel{Id = 4, JobTitle = "Teacher", Status=ApplicationStatus.Submitted},
            new ApplicationViewModel{Id = 5, JobTitle = "Cook", Status=ApplicationStatus.Submitted}
        };

        public IActionResult Index()
        {
            return View(applications);
        }

        public IActionResult Delete(int id)
        {
            return RedirectToAction("Index", "Application");
        }


        public IActionResult Details(int id, bool isEditing)
        {
            var model = new ApplicationDetailsViewModel
            {
                ApplicationModel = applications.Where(application => application.Id == id).FirstOrDefault(),
                IsEditing = isEditing
            };
            return View(model);
        }

        public IActionResult Apply(int id)
        {
            return RedirectToAction("Index", "Application");
        }

        [HttpPost]
        public IActionResult Save(int id, ApplicationDetailsViewModel model)
        {
            var oldModel = applications.Where(application => application.Id == id).FirstOrDefault();
            ApplicationViewModel newModel = new ApplicationViewModel();
            if (oldModel != null)
            {
                newModel = model.ApplicationModel;
                newModel.Status = ApplicationStatus.Undefined;
                newModel.JobTitle = oldModel.JobTitle;
                newModel.Id = oldModel.Id;

                if(oldModel.Status != ApplicationStatus.Undefined)
                {
                    oldModel.Status = ApplicationStatus.Withdrawn;
                    newModel.Id *= 10; //temporary
                    applications.Add(newModel);
                }
            }

            return RedirectToAction("Details", new { id = newModel.Id, isEditing = true });
        }
    }
}