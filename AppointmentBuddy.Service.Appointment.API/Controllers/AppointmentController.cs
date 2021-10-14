using AppointmentBuddy.Service.Appointment.API.Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;

namespace AppointmentBuddy.Service.Appointment.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{v:apiVersion}/api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(IAppointmentService appointmentService, ILogger<AppointmentController> logger)
        {
            _logger = logger;
            _appointmentService = appointmentService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("health")]
        public IActionResult Health()
        {
            return Ok(DateTime.Now.ToString());
        }

        [Route("appointment/{appointmentId}")]
        [HttpGet]
        public async Task<M.Appointment> GetAppointmentByAppointmentId(string appointmentId)
        {
            M.Appointment response;

            response = await _appointmentService.GetAppointmentByAppointmentId(appointmentId);

            return response;
        }

        [Route("all")]
        [HttpGet]
        public async Task<M.PaginatedResults<M.Appointment>> GetAllAppointments([FromQuery] string dateFrom = "", string dateTo = "", int pageIndex = 1, int pageSize = 10)
        {
            M.PaginatedResults<M.Appointment> response;

            response = await _appointmentService.GetAllAppointments(dateFrom, dateTo, pageIndex, pageSize);

            return response;
        }
    }
}
