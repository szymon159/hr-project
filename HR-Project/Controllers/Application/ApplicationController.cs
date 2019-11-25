using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Project.Enums;
using HR_Project.ViewModels;
using HR_Project_Database.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace HR_Project.Controllers.Application
{
    public class ApplicationController : Controller
    {
        private readonly DataContext context;
        private static List<ApplicationViewModel> applications;
        
        public ApplicationController(DataContext context)
        {
            this.context = context;
            
        }

        public IActionResult Index()
        {
            return View(applications);
        }

        public IActionResult Delete(int id)
        {
            var toDelete = applications.Where(offer => offer.Id == id);
            if (toDelete.Count() == 1)
            {
                toDelete.First().Status = ApplicationStatus.Withdrawn;
            }

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
            var application = applications.Where(item => item.Id == id).FirstOrDefault();

            if(application.Status == ApplicationStatus.Undefined)
                application.Status = ApplicationStatus.Submitted;

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