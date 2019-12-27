using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Project.DataLayer;
using HR_Project.ModelConverters;
using HR_Project.ViewModels;
using HR_Project.ViewModels.User;
using HR_Project_Database.EntityFramework;
using HR_Project_Database.Models;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<ActionResult> Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                if(!Program.IsUserChecked)
                {
                    var claims = User.Claims;

                    var user = new UserViewModel
                    {
                        FirstName = claims.ElementAt(1).Value,
                        LastName = claims.ElementAt(2).Value,
                        ExternalId = claims.ElementAt(0).Value,
                        Role = Enums.UserRole.User
                    };

                    User dbUser = context.User.FirstOrDefault(entity => entity.ExternalId == user.ExternalId);
                    if(dbUser == null)
                    {
                        context.User.Add(user.ToDatabaseModel());
                        await context.SaveChangesAsync();
                        Program.LoggedUserRole = Enums.UserRole.User;
                    }
                    else
                    {
                        Program.LoggedUserRole = (Enums.UserRole)dbUser.Role;
                    }
                    Program.IsUserChecked = true;
                }
            }
            else if(Program.IsUserChecked)
            {
                Program.IsUserChecked = false;
                Program.LoggedUserRole = Enums.UserRole.Unlogged;
            }

            return View(jobOffers);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var toDelete = context.JobOffer.Find(id);
            if(toDelete != null)
            {
                toDelete.Status = JobOfferStatus.Inactive;
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
                IsEditing = isEditing
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Save(int id, JobOfferDetailsViewModel model)
        {
            var toUpdate = context.JobOffer.Find(id);
            if (toUpdate != null)
            {
                toUpdate.Description = model.JobOfferModel.Description;
                await context.SaveChangesAsync();
            }

            return RedirectToAction("Details", new { id = id, isEditing = true });
        }
    }
}