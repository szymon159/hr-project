using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_Project.Controllers
{
    public class LoginController : ControllerBase
    {
        [HttpPost("signin")]
        //[Route("signin")]
        public IActionResult Login()
        {
            return RedirectToAction("Index", "JobOffer");
        }
    }
}