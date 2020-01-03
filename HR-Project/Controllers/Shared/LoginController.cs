using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HR_Project.Controllers.Shared
{
    //Necessary to add Claim with UserRole from HR-Project-Database to User.Claims fetched from Azure AD B2C
    //https://github.com/Azure-Samples/active-directory-b2c-dotnetcore-webapp/blob/master/WebApp-OpenIDConnect-DotNet/Controllers/SessionController.cs
    public class LoginController : Controller
    {
        public LoginController(IOptions<AzureAdB2COptions> b2cOptions)
        {
            AzureAdB2COptions = b2cOptions.Value;
        }

        public AzureAdB2COptions AzureAdB2COptions { get; set; }

        [HttpGet]
        public IActionResult SignIn()
        {
            var redirectUrl = Url.Action("Index", "JobOffer");
            return Challenge(new AuthenticationProperties { RedirectUri = redirectUrl }, OpenIdConnectDefaults.AuthenticationScheme);
        }

        [HttpGet]
        public IActionResult SignOut(bool passwordReset = false)
        {
            var callbackUrl = passwordReset ? Url.Action("PasswordResetSuccess", "Login") : Url.Action("Index", "JobOffer");
            return SignOut(new AuthenticationProperties { RedirectUri = callbackUrl }, CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            var redirectUrl = Url.Action("SignOut", "Login", new { passwordReset = true });
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            properties.Items[AzureAdB2COptions.PolicyAuthenticationProperty] = AzureAdB2COptions.ResetPasswordPolicyId;
            return Challenge(properties, OpenIdConnectDefaults.AuthenticationScheme);
        }

        [HttpGet]
        public IActionResult PasswordResetSuccess()
        {
            return View("PasswordResetSuccess");
        }
    }
}