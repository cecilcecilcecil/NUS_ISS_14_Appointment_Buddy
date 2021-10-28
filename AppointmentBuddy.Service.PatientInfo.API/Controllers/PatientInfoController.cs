using AppointmentBuddy.Core.Common.Helper;
using AppointmentBuddy.Service.PatientInfo.API.Core.Interface;
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

        [Route("patientinfo/byuser/{userId}")]
        [HttpGet]
        public async Task<M.PatientInfo> GetPatientInfoByUserId(string userId)
        {
            M.PatientInfo response;

            response = await _patientInfoService.GetPatientInfoByUserId(userId);

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

        [Route("save")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<int>> SavePatientInfo([FromBody] M.PatientInfo patinfo)
        {
            var success = Constants.ErrorCodes.Failure;

            if (patinfo == null)
            {
                return BadRequest();
            }

            success = await _patientInfoService.SavePatientInfo(patinfo);

            if (success == Constants.ErrorCodes.Failure)
            {
                return NoContent();
            }

            return success;
        }

        [Route("delete/{patId}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<int>> DeletePatientInfoById(string patId)
        {
            var success = Constants.ErrorCodes.Failure;

            success = await _patientInfoService.DeletePatientInfoById(patId);

            if (success == Constants.ErrorCodes.Failure)
            {
                return NoContent();
            }

            return success;
        }
    }
}
