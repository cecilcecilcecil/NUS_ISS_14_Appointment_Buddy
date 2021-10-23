﻿using AppointmentBuddy.Core.Common.Helper;
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
    }
}
