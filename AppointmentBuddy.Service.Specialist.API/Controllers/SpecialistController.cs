using AppointmentBuddy.Core.Common.Helper;
using AppointmentBuddy.Service.Specialist.API.Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;

namespace AppointmentBuddy.Service.Specialist.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{v:apiVersion}/api/[controller]")]
    [ApiController]
    [Authorize]
    public class SpecialistController : Controller
    {
        private readonly ISpecialistService _specialistService;
        private readonly ILogger<SpecialistController> _logger;

        public SpecialistController(ISpecialistService specialistService, ILogger<SpecialistController> logger)
        {
            _logger = logger;
            _specialistService = specialistService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("health")]
        public IActionResult Health()
        {
            return Ok(DateTime.Now.ToString());
        }
    }
}
