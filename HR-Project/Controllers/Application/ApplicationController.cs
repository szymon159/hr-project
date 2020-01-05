using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HR_Project.DataLayer;
using HR_Project.ExtensionMethods;
using HR_Project.ViewModels;
using HR_Project_Database.EntityFramework;
using HR_Project_Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR_Project.Controllers.Application
{
    [Authorize]
    public class ApplicationController : Controller
    {
        private readonly DataContext context;
        private static List<ApplicationViewModel> applications;
        
        public ApplicationController(IHttpContextAccessor contextAccessor, DataContext context)
        {
            var user = contextAccessor.HttpContext.User;
            var userExternalId = user.GetExternalId();
            var userRole = user.GetRole();

            this.context = context;
            applications = DatabaseReader.GetApplications(context, userExternalId, userRole);
        }

        public IActionResult Index()
        {
            return View(applications);
        }

        [Authorize(Roles =("User"))]
        public async Task<ActionResult> Delete(int id)
        {
            var toDelete = context.Application.Include(x => x.User).FirstOrDefault(x => x.IdApplication == id);
            if (toDelete != null && User.HasAccessToApplication(toDelete))
            {
                toDelete.Status = ApplicationStatus.Withdrawn;
                await context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Application");
        }

        // If user is HR, always return IsEditing = false, theoretically it might be possible for HR to edit details but they will never be saved
        [Authorize(Roles = ("User, HR"))]
        public IActionResult Details(int id, bool isEditing)
        {
            var baseModel = applications.FirstOrDefault(application => application.Id == id);
            
            // If no first name is loaded it means we have to fetch details
            if (string.IsNullOrEmpty(baseModel.FirstName))
                baseModel.GetApplicationDetails(context);

            if (!User.HasAccessToApplication(baseModel, context))
            {
                return RedirectToAction("Index", "Application");
            }

            var model = new ApplicationDetailsViewModel
            {
                ApplicationModel = baseModel,
                IsEditing = isEditing && !User.IsInRole(UserRole.HR.ToString())
            };
            return View(model);
        }

        [Authorize(Roles = ("User"))]
        public async Task<ActionResult> Apply(int id)
        {
            var toUpdate = context.Application.Include(x => x.User).FirstOrDefault(x => x.IdApplication == id);

            if (toUpdate?.Status == ApplicationStatus.Draft && User.HasAccessToApplication(toUpdate))
            {
                toUpdate.Status = ApplicationStatus.Submitted;
                await context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Application");
        }

        [HttpPost]
        [Authorize(Roles = ("User"))]
        public async Task<ActionResult> Save(int id, ApplicationDetailsViewModel viewModel)
        {
            // Find application to update in DB
            var toUpdate = context.Application.Include(x => x.User).FirstOrDefault(x => x.IdApplication == id);
            if(toUpdate == null || !User.HasAccessToApplication(toUpdate))
                return RedirectToAction("Index", "Application");

            if(toUpdate.Status != ApplicationStatus.Draft)
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
                    Status = ApplicationStatus.Draft
                };

                context.Application.Add(newModel);
                await context.SaveChangesAsync();
                id = newModel.IdApplication;
                toUpdate = newModel;
            }

            // If file with CV is selected, replace file in storage
            if (viewModel.ApplicationModel.CV != null && viewModel.ApplicationModel.CV?.Length != 0)
            {
                StorageContext.ReplaceCVFile(toUpdate.Cvid, viewModel.ApplicationModel.CV);
            }

            // If new attachments are selected, add them to DB (creating new AttachmentGroup) and storage
            if(viewModel.ApplicationModel.OtherAttachments != null && viewModel.ApplicationModel.OtherAttachments.Count != 0)
            {
                var attachmentGroup = new AttachmentGroup();
                context.AttachmentGroup.Add(attachmentGroup);
                await context.SaveChangesAsync();
                var groupId = toUpdate.AttachmentGroupId = attachmentGroup.IdAttachmentGroup;

                List<Attachment> attachments = new List<Attachment>();
                foreach(var attachmentFile in viewModel.ApplicationModel.OtherAttachments)
                {
                    attachments.Add(new Attachment { AttachmentGroupId = groupId, Extension = Path.GetExtension(attachmentFile.FileName) });
                }
                await context.AddRangeAsync(attachments);
                await context.SaveChangesAsync();

                StorageContext.UploadAttachmentGroup(attachments, viewModel.ApplicationModel.OtherAttachments);
            }

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