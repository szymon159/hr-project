﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Project.DataLayer;
using HR_Project.Enums;
using HR_Project.ExtensionMethods;
using HR_Project.ModelConverters;
using HR_Project.ViewModels;
using HR_Project_Database.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR_Project.Controllers
{
    public class JobOfferController : Controller
    {
        private readonly DataContext context;
        private static List<JobOfferViewModel> jobOffers;

        public JobOfferController(DataContext context)
        {
            this.context = context;
            jobOffers = DatabaseReader.GetJobOffers(context);
        }

        public IActionResult Index()
        {
            return View(jobOffers);
        }

        [Authorize(Roles = ("HR, Admin"))]
        public async Task<ActionResult> Delete(int id)
        {
            var toDelete = context.JobOffer
                .Include(x => x.Responsibility)
                .ThenInclude(x => x.User)
                .FirstOrDefault(jobOffer => jobOffer.IdJobOffer == id);           
            
            if(toDelete != null && User.CanManageJobOffer(toDelete))
            {
                toDelete.Status = HR_Project_Database.Models.JobOfferStatus.Inactive;
                await context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "JobOffer");
        }

        public IActionResult Details(int id, bool isEditing)
        {
            var baseModel = jobOffers.Where(offer => offer.Id == id).FirstOrDefault();
            if (baseModel.Description == null)
                baseModel.GetJobOfferDetails(context);

            var model = new JobOfferDetailsViewModel
            {
                JobOfferModel = baseModel,
                IsEditing = isEditing && User.CanManageJobOffer(baseModel)
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles ="HR, Admin")]
        public async Task<ActionResult> Save(int id, JobOfferDetailsViewModel model)
        {
            var toUpdate = context.JobOffer
                .Include(x => x.Responsibility)
                .ThenInclude(x => x.User)
                .FirstOrDefault(jobOffer => jobOffer.IdJobOffer == id);
            
            if (toUpdate != null && User.CanManageJobOffer(toUpdate))
            {
                toUpdate.Description = model.JobOfferModel.Description;
                await context.SaveChangesAsync();
            }

            return RedirectToAction("Details", new { id = id, isEditing = true });
        }

        [Authorize(Roles = "User")]
        public async Task<ActionResult> Apply(int id)
        {
            var jobOffer = context.JobOffer.Find(id);
            var newApplication = new HR_Project_Database.Models.Application()
            {
                JobOfferId = jobOffer.IdJobOffer,
                UserId = User.GetId(context),
                Cvid = 1, //TODO: allow adding application without CV
                Status = HR_Project_Database.Models.ApplicationStatus.Draft
            };

            context.Application.Add(newApplication);
            await context.SaveChangesAsync();

            return RedirectToAction("Details", "Application", new { id = newApplication.IdApplication, isEditing = true });
        }
    }
}