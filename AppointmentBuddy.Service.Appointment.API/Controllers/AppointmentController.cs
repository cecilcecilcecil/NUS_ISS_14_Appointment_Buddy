using AppointmentBuddy.Core.Common.Helper;
using AppointmentBuddy.Service.Appointment.API.Core.Interface;
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

        [Route("all/my")]
        [HttpGet]
        public async Task<M.PaginatedResults<M.Appointment>> GetAllMyAppointments([FromQuery] string dateFrom = "", string dateTo = "", string userId = "", int pageIndex = 1, int pageSize = 10)
        {
            M.PaginatedResults<M.Appointment> response;

            response = await _appointmentService.GetAllMyAppointments(dateFrom, dateTo, userId, pageIndex, pageSize);

            return response;
        }

        [Route("daterange")]
        [HttpGet]
        public async Task<IEnumerable<M.Appointment>> GetAllAppointmentsByDateRange([FromQuery] string dateFrom = "", string dateTo = "")
        {
            IEnumerable<M.Appointment> response;

            response = await _appointmentService.GetAllAppointmentsByDateRange(dateFrom, dateTo);

            return response;
        }

        [Route("available")]
        [HttpGet]
        public async Task<IEnumerable<M.Appointment>> GetAvailableAppointments()
        {
            IEnumerable<M.Appointment> response;

            response = await _appointmentService.GetAvailableAppointments();

            return response;
        }

        [Route("filtered")]
        [HttpPost]
        public async Task<List<string>> GetFilteredAppointmentsByPatientIds([FromBody] M.FilteredAppointment mf)
        {
            List<string> response;

            response = await _appointmentService.GetFilteredPatientIdsByDate(mf);

            return response;
        }

        [Route("save")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<int>> SaveAppointment([FromBody] M.Appointment appt)
        {
            var success = Constants.ErrorCodes.Failure;

            if (appt == null)
            {
                return BadRequest();
            }

            success = await _appointmentService.SaveAppointment(appt);

            if (success == Constants.ErrorCodes.Failure)
            {
                return NoContent();
            }

            return success;
        }
    }
}
