using AppointmentBuddy.Core.Common.Helper;
using AppointmentBuddy.Service.Services.API.Core.Interface;
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

namespace AppointmentBuddy.Service.Services.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{v:apiVersion}/api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServicesController : Controller
    {
        private readonly IServicesService _servicesService;
        private readonly ILogger<ServicesController> _logger;

        public ServicesController(IServicesService servicesService, ILogger<ServicesController> logger)
        {
            _logger = logger;
            _servicesService = servicesService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("health")]
        public IActionResult Health()
        {
            return Ok(DateTime.Now.ToString());
        }

        [Route("services/{svcId}")]
        [HttpGet]
        public async Task<M.Services> GetServiceByServicesId(string svcId)
        {
            M.Services response;

            response = await _servicesService.GetServiceByServicesId(svcId);

            return response;
        }

        [Route("all")]
        [HttpGet]
        public async Task<M.PaginatedResults<M.Services>> GetAllServices([FromQuery] string desc = "", int pageIndex = 1, int pageSize = 10)
        {
            M.PaginatedResults<M.Services> response;

            response = await _servicesService.GetAllServices(desc, pageIndex, pageSize);

            return response;
        }

        [Route("save")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<int>> SaveService([FromBody] M.Services svc)
        {
            var success = Constants.ErrorCodes.Failure;

            if (svc == null)
            {
                return BadRequest();
            }

            success = await _servicesService.SaveService(svc);

            if (success == Constants.ErrorCodes.Failure)
            {
                return NoContent();
            }

            return success;
        }
    }
}
