using AppointmentBuddy.Core.Common.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUS_ISS_14_Appointment_Buddy.Controllers.CustomException;
using NUS_ISS_14_Appointment_Buddy.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NUS_ISS_14_Appointment_Buddy.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) : base (logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("health")]
        public IActionResult Health()
        {
            return Ok(DateTime.Now.ToString());
        }

        [Route("home/index")]
        public IActionResult Index()
        {
            if (UserType == Constants.UserType.Admin || UserType == Constants.UserType.Staff)
            {
                return RedirectToAction("Index", "Admin");
            }


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature.Error is UserNotFoundException)
            {
                return View("NoAuthorization");
            }

            return View("ErrorPage");
        }

        [AllowAnonymous]
        public IActionResult NoAuthorization()
        {
            return View();
        }
    }
}
