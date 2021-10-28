using AppointmentBuddy.Core.Common.Config;
using AppointmentBuddy.Core.Common.Helper;
using AppointmentBuddy.Service.Identity.API.Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;

namespace AppointmentBuddy.Service.Identity.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{v:apiVersion}/api/[controller]")]
    [ApiController]
    [Authorize]
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;
        private readonly AppSettings _appSettings;
        private readonly ILogger<IdentityController> _logger;

        public IdentityController(IIdentityService identityService, IOptions<AppSettings> appSettings, ILogger<IdentityController> logger)
        {
            _identityService = identityService;
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("health")]
        public IActionResult Health()
        {
            return Ok(DateTime.Now.ToString());
        }

        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<M.IdentityDto>> Authenticate([FromBody] M.UserParameter userParam)
        {

            if (userParam == null)
            {
                return BadRequest();
            }

            var identityDto = await _identityService.Authenticate(userParam.Username, userParam.Password, userParam.UserTypeId);

            if (identityDto == null)
            {
                _logger.LogInformation("Failed username and password authentication.", userParam.Username);
                return NoContent();
            }

            return identityDto;
        }

        [Route("patients")]
        [HttpGet]
        public async Task<IEnumerable<M.User>> GetAllPatients()
        {
            IEnumerable<M.User> response;

            response = await _identityService.GetAllPatients();

            return response;
        }

        [Route("user/save")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<int>> SaveUser([FromBody] M.User patient)
        {
            var success = Constants.ErrorCodes.Failure;

            if (patient == null)
            {
                return BadRequest();
            }

            success = await _identityService.SaveUser(patient);

            if (success == Constants.ErrorCodes.Failure)
            {
                return NoContent();
            }

            return success;
        }
    }
}
