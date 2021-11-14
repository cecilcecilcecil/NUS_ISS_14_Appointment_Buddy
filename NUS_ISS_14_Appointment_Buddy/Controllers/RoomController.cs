using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUS_ISS_14_Appointment_Buddy.Interface;
using NUS_ISS_14_Appointment_Buddy.WEB.Config;
using M = AppointmentBuddy.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUS_ISS_14_Appointment_Buddy.Models;
using AppointmentBuddy.Core.Common.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NUS_ISS_14_Appointment_Buddy.Controllers
{
    public class RoomController : BaseController
    {
        private IRoomService _roomService;
        private IServicesService _servicesService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly ILogger<RoomController> _logger;

        public RoomController(IRoomService roomService, IServicesService servicesService, IOptions<AppSettings> appSettings,
            ILogger<RoomController> logger) : base(logger)
        {
            _roomService = roomService;
            _servicesService = servicesService;
            _appSettings = appSettings;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string desc)
        {
            ViewData["Description"] = desc;

            var allSvcs = await _servicesService.GetAllNonPageServices(AccessToken);

            var pageSize = _appSettings.Value.PageSize;
            var page = 1;

            M.PaginatedResults<M.Room> roomItems = await _roomService.GetAllRooms(AccessToken, desc, page, pageSize);

            foreach (var rm in roomItems.Data)
            {
                rm.ServicesName = allSvcs.FirstOrDefault(x => x.ServicesId == rm.SpecialiesId).Description;
            }

            var roomRvm = new ResultViewModel<M.Room>(roomItems.Data, roomItems.PageIndex, roomItems.PageSize, roomItems.Count);

            return View(roomRvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(string desc)
        {
            return Json(new
            {
                redirectUrl = Url.Action("Index", new { desc = desc })
            }); 
        }

        [HttpGet]
        public IActionResult LinkToAddRoom()
        {
            return Json(new { redirectUrl = Url.Action("AddRoom", "Room") });
        }

        [HttpGet]
        public async Task<IActionResult> AddRoom(string rmId = "")
        {
            Room model;

            if (!string.IsNullOrEmpty(rmId))
            {
                var room = await _roomService.GetRoomByRoomId(rmId, AccessToken);

                model = new Room
                {
                    RoomId = room.RoomId,
                    RoomName = room.RoomName,
                    SpecialiesId = room.SpecialiesId
                };
            }
            else
            {
                model = new Room
                {
                    RoomId = Guid.NewGuid().ToString(),
                };
            }

            ViewBag.Services = await GetServices();

            return View("AddRoom", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetRoomPage(int page, string specialiesId, string partialV)
        {
            var pageSize = _appSettings.Value.PageSize;

            var res = await _roomService.GetAllRooms(AccessToken, specialiesId, page, pageSize); ;

            object vm = new object();

            if (res != null)
            {
                vm = new ResultViewModel<M.Room>(res.Data, res.PageIndex, res.PageSize, res.Count);
            }

            return PartialView(partialV, vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveRoom(Room room)
        {
            string msgVal = "";

            M.Room coreRoom = new M.Room
            {
                RoomId = room.RoomId,
                RoomName = room.RoomName,
                SpecialiesId = room.SpecialiesId,
                LastUpdatedBy = UserName,
                LastUpdatedById = UserId
            };

            var successValue = await _roomService.SaveRoom(coreRoom, AccessToken);

            if (successValue == Constants.ErrorCodes.Success)
            {
                msgVal = "";
            }

            else
            {
                if (successValue == Constants.ErrorCodes.ConcurrencyError)
                {
                    msgVal = Constants.ValidationMessages.Concurrency;
                }

                else
                {
                    msgVal = Constants.ValidationMessages.SystemUnavailable;
                }
            }

            return Json(new { msgVal = msgVal, successVal = successValue });
        }

        public async Task<List<SelectListItem>> GetServices()
        {
            var returnList = new List<SelectListItem>();

            var services = await _servicesService.GetAllNonPageServices(AccessToken);

            foreach (var svc in services)
            {
                returnList.Add(
                    new SelectListItem
                    {
                        Text = svc.Description,
                        Value = svc.ServicesId
                    }
                );
            }

            return returnList;
        }
    }
}
