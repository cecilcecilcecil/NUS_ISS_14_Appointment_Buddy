using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUS_ISS_14_Appointment_Buddy.Interface;
using NUS_ISS_14_Appointment_Buddy.WEB.Config;
using AppointmentBuddy.Core.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using AppointmentBuddy.WEB.Models.Account;
using System.Security.Claims;
using AppointmentBuddy.Core.Common.Helper;
using static AppointmentBuddy.Core.Common.Helper.Constants;

namespace NUS_ISS_14_Appointment_Buddy.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<AccountController> _logger;

        private readonly WEB.Config.AppSettings _appSettings;
        private readonly IOptions<ServiceUrls> _serviceUrls;

        public AccountController(IIdentityService identityService, ILogger<AccountController> logger,
            IOptions<WEB.Config.AppSettings> appSettings, IOptions<ServiceUrls> serviceUrls) : base(logger)
        {
            _logger = logger;
            _identityService = identityService;
            _appSettings = appSettings.Value;
            _serviceUrls = serviceUrls;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return RedirectToAction("AccountLogin", "Account");
        }

        [HttpGet]
        [Route("")]
        public IActionResult AccountLogin()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            HttpContext.Response.Headers.Remove("X-Frame-Options");
            return View();
        }

        public IActionResult StaffGoToLogin()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("StaffLogin");
        }

        [HttpPost]
        public IActionResult StaffMockSignIn(LoginViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("StaffLandingPage", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessStaffLogin(LoginViewModel model)
        {
            var identityDto = await _identityService.Authenticate(model.UserName, model.Password, "");

            if (identityDto == null)
            {
                return Json(new { result = Constants.ErrorCodes.NotAuthorized });
            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, identityDto.UserInfo.User.Name));
            identity.AddClaim(new Claim(Constants.AppClaimTypes.Id, identityDto.UserInfo.User.UserId));
            identity.AddClaim(new Claim(Constants.AppClaimTypes.UserType, identityDto.UserInfo.User.UserTypeId));

            switch (identityDto.UserInfo.User.UserTypeId)
            {
                case Constants.UserType.Staff:
                    identity.AddClaim(new Claim(ClaimTypes.Role, identityDto.UserInfo.Role.RoleTypeId));
                    break;
                case Constants.UserType.Admin:
                    identity.AddClaim(new Claim(ClaimTypes.Role, Constants.RoleType.Admin));
                    break;
            }

            identity.AddClaim(new Claim(AppClaimTypes.AccessToken, identityDto.Token));

            await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity)).ConfigureAwait(false);

            return Json(new { result = ErrorCodes.Success });
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await ClearSignInSessionAsync().ConfigureAwait(false);

            return RedirectToAction(nameof(AccountController.Login), "Account");
        }

        private async Task ClearSignInSessionAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).ConfigureAwait(false);
        }
    }
}
