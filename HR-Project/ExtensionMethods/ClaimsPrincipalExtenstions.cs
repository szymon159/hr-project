using HR_Project.DataLayer;
using HR_Project.Enums;
using HR_Project.ViewModels;
using HR_Project_Database.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HR_Project.ExtensionMethods
{
    public static class ClaimsPrincipalExtenstions
    {
        public static string GetFirstName(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.GivenName);
        }

        public static string GetLastName(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Surname);
        }

        public static string GetExternalId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static string GetRole(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Role);
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            // Azure AD B2C does not always follow standards regarding email type :)
            return user.Claims.FirstOrDefault(x => ClaimTypes.Email == x.Type || x.Type == "email" || x.Type == "emails")?.Value;
        }

        public static bool IsInRole(this ClaimsPrincipal user, UserRole role)
        {
            return user.IsInRole(role.ToString());
        }

        public static bool HasAccessToApplication(this ClaimsPrincipal user, HR_Project_Database.Models.Application application)
        {
            if(user.IsInRole(UserRole.User))
            {
                return user.GetExternalId() == application.User.ExternalId;
            }
            else if(user.IsInRole(UserRole.HR))
            {
                return application.JobOffer.Responsibility.Any(responsibility => responsibility.User.ExternalId == user.GetExternalId());
            }

            return false;
        }

        public static bool HasAccessToApplication(this ClaimsPrincipal user, ApplicationViewModel application, DataContext context)
        {
            if (user.IsInRole(UserRole.User) || user.IsInRole(UserRole.HR))
            {
                var fetchedApplication = context.Application
                    .Include(x => x.User)
                    .Include(x => x.JobOffer.Responsibility)
                    .FirstOrDefault(dbApplication => dbApplication.IdApplication == application.Id);

                return user.HasAccessToApplication(fetchedApplication);
            }

            return false;
        }
    }
}
