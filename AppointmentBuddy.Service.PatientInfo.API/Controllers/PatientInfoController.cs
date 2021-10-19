using AppointmentBuddy.Service.PatientInfo.API.Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;

namespace AppointmentBuddy.Service.PatientInfo.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{v:apiVersion}/api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientInfoController : Controller
    {
        private readonly IPatientInfoService _patientInfoService;
        private readonly ILogger<PatientInfoController> _logger;

        public PatientInfoController(IPatientInfoService patientInfoService, ILogger<PatientInfoController> logger)
        {
            _logger = logger;
            _patientInfoService = patientInfoService;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("health")]
        public IActionResult Health()
        {
            return Ok(DateTime.Now.ToString());
        }

        [Route("patientinfo/{patId}")]
        [HttpGet]
        public async Task<M.PatientInfo> GetPatientInfoById(string patId)
        {
            M.PatientInfo response;

            response = await _patientInfoService.GetPatientInfoById(patId);

            return response;
        }

        [Route("search")]
        [HttpGet]
        public async Task<M.PaginatedResults<M.PatientInfo>> GetPatientInfoBySearch([FromQuery] string nric = "", string patName = "", int pageIndex = 1, int pageSize = 10)
        {
            M.PaginatedResults<M.PatientInfo> response;

            response = await _patientInfoService.GetPatientInfoBySearch(nric, patName, pageIndex, pageSize);

            return response;
        }

    }
}
