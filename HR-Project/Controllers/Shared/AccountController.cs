using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HR_Project.Controllers.Shared
{
    public class AccountController : Controller
    {
        public IActionResult AccessDenied()
        {
            var exception = new UnauthorizedAccessException("You have no access to requested action. Try to Sign Out and Sign In again in order to update data. Contact your administrator if problem persist.");

            return View("Error", new ErrorViewModel() { Exception = exception });
        }
    }
}