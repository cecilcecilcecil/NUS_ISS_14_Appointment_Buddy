using AppointmentBuddy.Core.Common.Helper;
using AppointmentBuddy.Service.Room.API.Core.Interface;
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
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly ILogger<RoomController> _logger;

        public RoomController(IRoomService roomService, ILogger<RoomController> logger)
        {
            _logger = logger;
            _roomService = roomService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("health")]
        public IActionResult Health()
        {
            return Ok(DateTime.Now.ToString());
        }

        [Route("room/{roomId}")]
        [HttpGet]
        public async Task<M.Room> GetRoomByRoomId(string roomId)
        {
            M.Room response;

            response = await _roomService.GetRoomByRoomId(roomId);

            return response;
        }

        [Route("all")]
        [HttpGet]
        public async Task<M.PaginatedResults<M.Room>> GetAllRooms([FromQuery] string desc = "", int pageIndex = 1, int pageSize = 10)
        {
            M.PaginatedResults<M.Room> response;

            response = await _roomService.GetAllRooms(desc, pageIndex, pageSize);

            return response;
        }

        [Route("save")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<int>> SaveRoom([FromBody] M.Room rm)
        {
            var success = Constants.ErrorCodes.Failure;

            if (rm == null)
            {
                return BadRequest();
            }

            success = await _roomService.SaveRoom(rm);

            if (success == Constants.ErrorCodes.Failure)
            {
                return NoContent();
            }

            return success;
        }
    }
}
