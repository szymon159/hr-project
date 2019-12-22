using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Project.DataLayer;
using HR_Project.ViewModels;
using HR_Project_Database.EntityFramework;
using HR_Project_Database.Models;
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
            applications = DatabaseReader.GetApplications(context);
        }

        public IActionResult Index()
        {
            return View(applications);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var toDelete = context.Application.Find(id);
            if (toDelete != null)
            {
                toDelete.Status = ApplicationStatus.Withdrawn;
                await context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Application");
        }


        public IActionResult Details(int id, bool isEditing)
        {
            var baseModel = applications.Where(offer => offer.Id == id).FirstOrDefault();
            if (string.IsNullOrEmpty(baseModel.FirstName))
                baseModel.GetApplicationDetails(context);

            var model = new ApplicationDetailsViewModel
            {
                ApplicationModel = baseModel,
                IsEditing = isEditing
            };
            return View(model);
        }

        public async Task<ActionResult> Apply(int id)
        {
            var toUpdate = context.Application.Find(id);

            if(toUpdate?.Status == ApplicationStatus.Undefined)
            {
                toUpdate.Status = ApplicationStatus.Submitted;
                await context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Application");
        }

        [HttpPost]
        public async Task<ActionResult> Save(int id, ApplicationDetailsViewModel viewModel)
        {
            // Find application to update in DB
            var toUpdate = context.Application.Find(id);
            if(toUpdate == null)
                return RedirectToAction("Index", "Application");

            if(toUpdate.Status != ApplicationStatus.Undefined)
            {
                // If application was already submitted, withdraw it
                toUpdate.Status = ApplicationStatus.Withdrawn;
                await context.SaveChangesAsync();

                // And create new application with data copied from previous one
                var newModel = new HR_Project_Database.Models.Application
                {
                    JobOfferId = toUpdate.JobOfferId,
                    UserId = toUpdate.UserId,
                    Cvid = toUpdate.Cvid,
                    AttachmentGroupId = toUpdate.AttachmentGroupId,
                    ApplicationMessageId = toUpdate.ApplicationMessageId,
                    Status = ApplicationStatus.Undefined
                };

                context.Application.Add(newModel);
                await context.SaveChangesAsync();
                id = newModel.IdApplication;
                toUpdate = newModel;
            }

            // Replace CV if necessary - only in storage
            if (viewModel.ApplicationModel.CV.Length != 0)
            {
                StorageContext.ReplaceCVFile(toUpdate.Cvid, viewModel.ApplicationModel.CV);
            }

            // TODO: Add support for attaching files

            // Replace message if necessary
            ApplicationMessage message;
            if ((message = context.ApplicationMessage.Find(toUpdate.ApplicationMessageId)) == null)
            {
                if(viewModel.ApplicationModel.Message == null)
                    return RedirectToAction("Details", new { id = id, isEditing = true });

                message = new ApplicationMessage { MessageContent = viewModel.ApplicationModel.Message };
                context.ApplicationMessage.Add(message);
                await context.SaveChangesAsync();
                toUpdate.ApplicationMessageId = message.IdApplicationMessage;
            }
            else
            {
                message.MessageContent = viewModel.ApplicationModel.Message;
            }
            await context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = id, isEditing = true });
        }
    }
}