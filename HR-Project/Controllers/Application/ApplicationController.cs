﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HR_Project.DataLayer;
using HR_Project.ExtensionMethods;
using HR_Project.Notifications;
using HR_Project.ViewModels;
using HR_Project_Database.EntityFramework;
using HR_Project_Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SendGrid;

namespace HR_Project.Controllers.Application
{
    [Authorize]
    public class ApplicationController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly DataContext context;
        
        public ApplicationController(IConfiguration configuration, IHttpContextAccessor contextAccessor, DataContext context)
        {
            this.configuration = configuration;
            this.context = context;
        }

        public IActionResult Index(int id = 0)
        {
            var userExternalId = User.GetExternalId();
            var userRole = User.GetRole();
            var applications = DatabaseReader.GetApplications(context, userExternalId, userRole, false, id);

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
            var baseModel = DatabaseReader.GetApplications(context, User.GetExternalId(), User.GetRole(), true)
                .FirstOrDefault(application => application.Id == id);
            
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
            var toUpdate = context.Application
                .Include(x => x.User)
                .FirstOrDefault(x => x.IdApplication == id);

            if(toUpdate.CvId == null)
                return RedirectToAction("Details", new { id = id, isEditing = true });

            if (toUpdate?.Status == ApplicationStatus.Draft 
                && User.HasAccessToApplication(toUpdate))
            {
                toUpdate.Status = ApplicationStatus.Submitted;
                await context.SaveChangesAsync();
            }

            //Send notification
            var apiKey = configuration.GetSection("SENDGRID_API_KEY").Value;
            var client = new SendGridClient(apiKey);
            var jobOffer = await context.JobOffer
                .Include(x => x.Responsibility)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(offer => offer.IdJobOffer == toUpdate.JobOfferId);
            var message = NotificationHelper.CreateNotificationForOffer(jobOffer);
            await client.SendEmailAsync(message);
            //

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
                    CvId = toUpdate.CvId,
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
                // If no file in storage, generate new id for CV
                if (toUpdate.CvId == null)
                {
                    toUpdate.CvId = Guid.NewGuid();
                    await context.SaveChangesAsync();
                }

                StorageContext.ReplaceCVFile(toUpdate.CvId.Value, viewModel.ApplicationModel.CV);
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

        [Authorize(Roles = ("HR"))]
        public async Task<ActionResult> Approve(int id)
        {
            var toApprove = context.Application
                .Include(x => x.User)
                .Include(x => x.JobOffer.Responsibility)
                .FirstOrDefault(x => x.IdApplication == id);
            
            if (toApprove != null
                && User.HasAccessToApplication(toApprove)
                && toApprove.Status != ApplicationStatus.Draft
                && toApprove.Status != ApplicationStatus.Withdrawn)
            {
                toApprove.Status = ApplicationStatus.Approved;
                await context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Application");
        }

        [Authorize(Roles = ("HR"))]
        public async Task<ActionResult> Reject(int id)
        {
            var toReject = context.Application
                .Include(x => x.User)
                .Include(x => x.JobOffer.Responsibility)
                .FirstOrDefault(x => x.IdApplication == id);
            
            if (toReject != null 
                && User.HasAccessToApplication(toReject)
                && toReject.Status != ApplicationStatus.Draft 
                && toReject.Status != ApplicationStatus.Withdrawn)
            {
                toReject.Status = ApplicationStatus.Rejected;
                await context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Application");
        }

        public async Task<FileContentResult> DownloadCv(int id)
        {
            var application = context.Application
                .Include(x => x.User)
                .Include(x => x.JobOffer.Responsibility)
                .FirstOrDefault(x => x.IdApplication == id);

            string fileName = application.CvId.ToString() + ".pdf";
            byte[] fileContent = null;
            string contentType = null;
            if (User.HasAccessToApplication(application) &&  application.CvId != null)
            {
                (fileContent, contentType) = await StorageContext.DownloadCVFileAsync(application.CvId.Value);
            }

            return new FileContentResult(fileContent, contentType) { FileDownloadName = fileName };
        }

        public async Task<FileContentResult> DownloadAttachment(int id, string filePath)
        {
            var application = context.Application
                .Include(x => x.User)
                .Include(x => x.JobOffer.Responsibility)
                .FirstOrDefault(x => x.IdApplication == id);

            byte[] fileContent = null;
            string contentType = null;
            if (User.HasAccessToApplication(application))
            {
                (fileContent, contentType) = await StorageContext.DownloadAttachmentAsync(filePath);
            }

            return new FileContentResult(fileContent, contentType) { FileDownloadName = filePath };
        }
    }
}