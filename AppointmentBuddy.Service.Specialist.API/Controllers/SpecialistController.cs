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

        [Route("specialist/{specId}")]
        [HttpGet]
        public async Task<M.Specialist> GetSpecialistById(string specId)
        {
            M.Specialist response;

            response = await _specialistService.GetSpecialistById(specId);

            return response;
        }

        [Route("search")]
        [HttpGet]
        public async Task<M.PaginatedResults<M.Specialist>> GetSpecialistBySearch([FromQuery] string nric = "", string specName = "", int pageIndex = 1, int pageSize = 10)
        {
            M.PaginatedResults<M.Specialist> response;

            response = await _specialistService.GetSpecialistBySearch(nric, specName, pageIndex, pageSize);

            return response;
        }

        [Route("save")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<int>> SaveSpecialist([FromBody] M.Specialist specInfo)
        {
            var success = Constants.ErrorCodes.Failure;

            if (specInfo == null)
            {
                return BadRequest();
            }

            success = await _specialistService.SaveSpecialist(specInfo);

            if (success == Constants.ErrorCodes.Failure)
            {
                return NoContent();
            }

            return success;
        }

        [Route("delete/{specId}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<int>> DeleteSpecialistById(string specId)
        {
            var success = Constants.ErrorCodes.Failure;

            success = await _specialistService.DeleteSpecialistById(specId);

            if (success == Constants.ErrorCodes.Failure)
            {
                return NoContent();
            }

            return success;
        }
    }
}
