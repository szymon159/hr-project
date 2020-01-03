using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HR_Project.Controllers.Shared
{
    public class ErrorController : Controller
    {
        public IActionResult Error(string message, string requestId = null)
        {
            return View("Error", new ErrorViewModel() { Exception = new ApplicationException(message), RequestId = requestId });
        }
    }
}