using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using HR_Project.DataLayer;
using HR_Project.Enums;
using HR_Project.ExtensionMethods;
using HR_Project.ModelConverters;
using HR_Project.ViewModels;
using HR_Project_Database.EntityFramework;
using HR_Project_Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HR_Project.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        DataContext context;

        public UsersController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("get")]
        public List<UserViewModel> GetUsers()
        {
            return DatabaseReader.GetUsers(context);
        }

        [Route("Promote/{id}")]
        [HttpGet]
        public async Task<ActionResult> Promote([FromRoute]int id)
        {
            var user = context.User
                .Include(x => x.Responsibility)
                .FirstOrDefault(dbUser => dbUser.IdUser == id);
            if(user != null)
            {
                switch(user.Role)
                {
                    case HR_Project_Database.Models.UserRole.User:
                        user.Role = HR_Project_Database.Models.UserRole.HR;
                        await context.SaveChangesAsync();
                        break;
                    case HR_Project_Database.Models.UserRole.HR:
                        user.Role = HR_Project_Database.Models.UserRole.Admin;
                        context.Responsibility.RemoveRange(user.Responsibility);
                        await context.SaveChangesAsync();
                        break;
                }
            }

            return RedirectToAction("Index");
        }

        [Route("Degrade/{id}")]
        [HttpGet]
        public async Task<ActionResult> Degrade([FromRoute]int id)
        {
            var user = context.User
                .Include(x => x.Responsibility)
                .FirstOrDefault(dbUser => dbUser.IdUser == id);
            if (user != null)
            {
                switch (user.Role)
                {
                    case HR_Project_Database.Models.UserRole.Admin:
                        user.Role = HR_Project_Database.Models.UserRole.HR;
                        await context.SaveChangesAsync();
                        break;
                    case HR_Project_Database.Models.UserRole.HR:
                        user.Role = HR_Project_Database.Models.UserRole.User;
                        context.Responsibility.RemoveRange(user.Responsibility);
                        await context.SaveChangesAsync();
                        break;
                }
            }

            return RedirectToAction("Index");
        }

        [Route("ManageOffers/{id}")]
        [HttpGet]
        public IActionResult ManageOffers([FromRoute] int id)
        {
            return View(context.User.Find(id).ToViewModel());
        }

        [HttpGet("ManageOffers/get/{id}")]
        public List<ManagedJobOfferViewModel> GetJobOffersForUser([FromRoute] int id)
        {
            return context.JobOffer
                .Include(x => x.Responsibility)
                .Select(jobOffer => new ManagedJobOfferViewModel()
                {
                    JobOfferId = jobOffer.IdJobOffer,
                    JobOfferTitle = jobOffer.JobTitle,
                    IsManaged = jobOffer.Responsibility.Any(x => x.UserId == id)
                }).ToList();
        }

        [HttpPost("ManageOffers/post/{id}")]
        public async Task<ActionResult> SaveJobOffers([FromRoute] int id)
        {
            var checkedJobOffers = new List<string>();
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                string rawValue = reader.ReadToEnd();

                var decoded = WebUtility.UrlDecode(rawValue).Replace("\"", "").Replace("\\", "");
                var value = decoded.Substring(decoded.IndexOf("=[") + 2);
                checkedJobOffers = value.Remove(value.Length - 1).Split(',').ToList();
            }

            var oldResponsibilities = context.Responsibility
                .Where(responsiblity => responsiblity.UserId == id);

            var oldJobOffers = oldResponsibilities.Select(resp => resp.JobOfferId);

            var toDelete = new List<Responsibility>();
            foreach (var responsibility in oldResponsibilities)
            {
                if (!checkedJobOffers.Contains(responsibility.JobOfferId.ToString()))
                {
                    toDelete.Add(responsibility);
                }
            }

            var newJobOffers = new List<string>();
            foreach (var newJobOffer in checkedJobOffers)
            {
                if (!oldJobOffers.Contains(int.Parse(newJobOffer)))
                    newJobOffers.Add(newJobOffer);
            }

            context.Responsibility.RemoveRange(toDelete);
            await context.SaveChangesAsync();

            var toAdd = newJobOffers.Select(jobOfferId => new Responsibility()
            {
                JobOfferId = int.Parse(jobOfferId),
                UserId = id
            });

            context.Responsibility.AddRange(toAdd);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}