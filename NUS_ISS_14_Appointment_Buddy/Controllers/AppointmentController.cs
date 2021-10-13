using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUS_ISS_14_Appointment_Buddy.Interface;
using NUS_ISS_14_Appointment_Buddy.WEB.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NUS_ISS_14_Appointment_Buddy.Controllers
{
    public class AppointmentController : BaseController
    {
        private IAppointmentService _appointmentService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(IAppointmentService appointmentService, IOptions<AppSettings> appSettings, 
            ILogger<AppointmentController> logger) : base(logger)
        {
            _appointmentService = appointmentService;
            _appSettings = appSettings;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string apptId = "")
        {
            var appt = await _appointmentService.GetAppointmentByAppointmentId("1B8FA93B-29E8-4E44-A0B3-A1AB5B8E1458", AccessToken);

            return View(appt);
        }
    }
}
