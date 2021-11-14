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
    public class AppointmentController : BaseController
    {
        private IAppointmentService _appointmentService;
        private IServicesService _servicesService;
        private IIdentityService _identityService;
        private IRoomService _roomService;
        private ISpecialistService _specialistService;

        private readonly IOptions<AppSettings> _appSettings;
        private readonly ILogger<AppointmentController> _logger;
        private const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public AppointmentController(IAppointmentService appointmentService, IIdentityService identityService, IRoomService roomService, IServicesService servicesService,
            ISpecialistService specialistService,
            IOptions<AppSettings> appSettings, 
            ILogger<AppointmentController> logger) : base(logger)
        {
            _appointmentService = appointmentService;
            _servicesService = servicesService;
            _roomService = roomService;
            _specialistService = specialistService;
            _identityService = identityService;
            _appSettings = appSettings;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index(string dateFrom, string dateTo)
        {
            ViewData["DateFrom"] = dateFrom;
            ViewData["DateTo"] = dateTo;

            var pageSize = _appSettings.Value.PageSize;
            var page = 1;

            M.PaginatedResults<M.Appointment> apptItems = await _appointmentService.GetAllAppointments(AccessToken, dateFrom, dateTo, page, pageSize);

            foreach (var appt in apptItems.Data)
            {
                if (!string.IsNullOrEmpty(appt.ServiceId))
                {
                    appt.ServiceName = (await _servicesService.GetServiceByServicesId(appt.ServiceId, AccessToken)).Description;
                    appt.RoomName = (await _roomService.GetRoomByRoomId(appt.RoomId, AccessToken)).RoomName;
                    appt.SpecialistName = (await _specialistService.GetSpecialistById(appt.SpecialistId, AccessToken)).Name;
                }
            }

            var apptRvm = new ResultViewModel<M.Appointment>(apptItems.Data, apptItems.PageIndex, apptItems.PageSize, apptItems.Count);

            return View(apptRvm);
        }

        [HttpGet]
        public async Task<IActionResult> ViewMyAppointments(string dateFrom, string dateTo)
        {
            ViewData["DateFrom"] = dateFrom;
            ViewData["DateTo"] = dateTo;

            var pageSize = _appSettings.Value.PageSize;
            var page = 1;

            M.PaginatedResults<M.Appointment> apptItems = await _appointmentService.GetAllMyAppointments(AccessToken, dateFrom, dateTo, UserId, page, pageSize);

            foreach (var appt in apptItems.Data)
            {
                if (!string.IsNullOrEmpty(appt.ServiceId))
                {
                    appt.ServiceName = (await _servicesService.GetServiceByServicesId(appt.ServiceId, AccessToken)).Description;
                    appt.RoomName = (await _roomService.GetRoomByRoomId(appt.RoomId, AccessToken)).RoomName;
                    appt.SpecialistName = (await _specialistService.GetSpecialistById(appt.SpecialistId, AccessToken)).Name;
                }
            }
            var apptRvm = new ResultViewModel<M.Appointment>(apptItems.Data, apptItems.PageIndex, apptItems.PageSize, apptItems.Count);

            return View("Index", apptRvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(string dateFrom, string dateTo)
        {
            return Json(new
            {
                redirectUrl = Url.Action("Index", new { dateFrom = dateFrom, dateTo = dateTo })
            });
        }

        [HttpGet]
        public IActionResult LinkToAddAppointment()
        {
            return Json(new { redirectUrl = Url.Action("AddAppointment", "Appointment") });
        }

        [HttpGet]
        public async Task<IActionResult> AddAppointment(string apptId = "")
        {
            Appointment model;

            if (!string.IsNullOrEmpty(apptId))
            {
                var appt = await _appointmentService.GetAppointmentByAppointmentId(apptId, AccessToken);

                model = new Appointment
                {
                    AppointmentId = appt.AppointmentId,
                    AppointmentDate = appt.AppointmentDate.GetValueOrDefault().ToString("dd/MM/yyyy"),
                    AppointmentTime = appt.AppointmentTime,
                    Name = appt.Name,
                    UserId = appt.UserId
                };
            }
            else
            {
                model = new Appointment
                {
                    AppointmentId = Guid.NewGuid().ToString(),
                };
            }
           
            return View("AddAppointment", model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAppointment(string apptId = "")
        {
            Appointment model;

            if (string.IsNullOrEmpty(apptId))
            {
                return RedirectToAction("NoAuthorization", "Home");
            }
            else
            {
                var appt = await _appointmentService.GetAppointmentByAppointmentId(apptId, AccessToken);

                model = new Appointment
                {
                    AppointmentId = appt.AppointmentId,
                    AppointmentDate = appt.AppointmentDate.GetValueOrDefault().ToString("dd/MM/yyyy"),
                    AppointmentTime = appt.AppointmentTime,
                    ServiceId = appt.ServiceId,
                    SpecialistId = appt.SpecialistId,
                    RoomId = appt.RoomId,
                    Name = appt.Name,
                    UserId = appt.UserId
                };

                ViewBag.Patients = await GetFilteredPatients(appt.AppointmentDate.GetValueOrDefault(), appt.AppointmentTime, appt.AppointmentId);
                ViewBag.Services = await GetServices();

                if (!string.IsNullOrEmpty(appt.ServiceId))
                {
                    ViewBag.Specialists = await GetSpecialistSelectListItem(appt.ServiceId);
                    ViewBag.Rooms = await GetRoomSelectListItem(appt.ServiceId);
                }
                else
                {
                    ViewBag.Specialists = new List<SelectListItem>();
                    ViewBag.Rooms = new List<SelectListItem>();
                }
            }

            return View("UpdateAppointment", model);
        }

        [HttpGet]
        public async Task<IActionResult> RescheduleAppointment(string apptId = "")
        {
            Appointment model;

            if (string.IsNullOrEmpty(apptId))
            {
                return RedirectToAction("NoAuthorization", "Home");
            }
            else
            {
                var appt = await _appointmentService.GetAppointmentByAppointmentId(apptId, AccessToken);

                model = new Appointment
                {
                    AppointmentId = appt.AppointmentId,
                    AppointmentDate = appt.AppointmentDate.GetValueOrDefault().ToString("dd/MM/yyyy"),
                    AppointmentTime = appt.AppointmentTime,
                    Name = appt.Name,
                    UserId = appt.UserId
                };

                ViewBag.AvailableAppointments = await GetAvailableAppointments(apptId);
            }

            return View("RescheduleAppointment", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointmentPage(int page, string dateFrom, string dateTo, string partialV)
        {
            var pageSize = _appSettings.Value.PageSize;

            var res = await _appointmentService.GetAllAppointments(AccessToken, dateFrom, dateTo, page, pageSize);

            object vm = new object();

            if (res != null)
            {
                vm = new ResultViewModel<M.Appointment>(res.Data, res.PageIndex, res.PageSize, res.Count);
            }

            return PartialView(partialV, vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAppointment(Appointment appt)
        {
            string msgVal = "";

            bool apptDateValid = DateTime.TryParse(appt.AppointmentDate, out DateTime apptDate);

            M.Appointment coreAppt = new M.Appointment
            {
                AppointmentDate = apptDate,
                AppointmentId = appt.AppointmentId,
                AppointmentTime = appt.AppointmentTime,
                ServiceId = appt.ServiceId,
                SpecialistId = appt.SpecialistId,
                RoomId = appt.RoomId,
                Name = appt.Name,
                UserId = appt.UserId,
                LastUpdatedBy = UserName,
                LastUpdatedById = UserId
            };

            var successValue = await _appointmentService.SaveAppointment(coreAppt, AccessToken);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelAppointment(Appointment appt)
        {
            string msgVal = "";

            bool apptDateValid = DateTime.TryParse(appt.AppointmentDate, out DateTime apptDate);

            M.Appointment coreAppt = new M.Appointment
            {
                AppointmentDate = apptDate,
                AppointmentId = appt.AppointmentId,
                AppointmentTime = appt.AppointmentTime,
                Name = null,
                UserId = null,
                LastUpdatedBy = UserName,
                LastUpdatedById = UserId
            };

            var successValue = await _appointmentService.SaveAppointment(coreAppt, AccessToken);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetNewAppointment(Appointment appt)
        {
            string msgVal = "";

            bool apptDateValid = DateTime.TryParse(appt.AppointmentDate, out DateTime apptDate);

            M.Appointment coreAppt = new M.Appointment
            {
                AppointmentDate = apptDate,
                AppointmentId = appt.AppointmentId,
                AppointmentTime = appt.AppointmentTime,
                Name = null,
                UserId = null,
                LastUpdatedBy = UserName,
                LastUpdatedById = UserId
            };

            var successValue = await _appointmentService.SaveAppointment(coreAppt, AccessToken);

            var newAppt = await _appointmentService.GetAppointmentByAppointmentId(appt.NewAppointmentId, AccessToken);

            newAppt.UserId = UserId;
            newAppt.Name = UserName;

            successValue = await _appointmentService.SaveAppointment(newAppt, AccessToken);

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

        public async Task<List<SelectListItem>> GetFilteredPatients(DateTime date, string time, string apptId)
        {
            IEnumerable<M.User> patients = new List<M.User>();

            patients = await _identityService.GetAllPatients(AccessToken);

            if (patients != null && patients.Count() > 0)
            {
                M.FilteredAppointment mf = new M.FilteredAppointment
                {
                    AppointmentDate = date,
                    AppointmentTime = time,
                    AppointmentId = apptId
                };

                var filteredIds = await _appointmentService.GetFilteredAppointmentsByPatientIds(mf, AccessToken);

                if (filteredIds != null && filteredIds.Count() > 0)
                {
                    patients = patients.Where(y => !filteredIds.Contains(y.UserId)).ToList();
                }
            }

            List<SelectListItem> selList = new List<SelectListItem>();

            foreach (var pat in patients)
            {
                selList.Add(
                    new SelectListItem
                    {
                        Text = pat.Name,
                        Value = pat.UserId
                    }
                );
            }

            return selList;
        }

        public async Task<List<SelectListItem>> GetAvailableAppointments(string apptId)
        {
            var appts = await _appointmentService.GetAvailableAppointments(AccessToken);

            List<SelectListItem> selList = new List<SelectListItem>();

            foreach (var appt in appts)
            {
                if (appt.AppointmentId != apptId)
                {
                    selList.Add(
                        new SelectListItem
                        {
                            Text = appt.AppointmentDate.GetValueOrDefault().ToString("dd/MM/yyyy") + " " + appt.AppointmentTime,
                            Value = appt.AppointmentId
                        }
                    );
                }
            }

            return selList;
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

        public async Task<JsonResult> GetSpecialistByServiceId(string serviceId)
        {
            var returnList = await GetSpecialistSelectListItem(serviceId);

            return Json(returnList);
        }

        public async Task<List<SelectListItem>> GetSpecialistSelectListItem(string serviceId)
        {
            var returnList = new List<SelectListItem> { new SelectListItem { Text = "Please select service", Value = "" } };

            var resultList = await _specialistService.GetSpecialistByServiceId(serviceId, AccessToken);

            if (resultList != null && resultList.Count() > 0)
            {
                foreach (var subcategoryItem in resultList.OrderBy(item => item.Name))
                {
                    returnList.Add(new SelectListItem() { Text = subcategoryItem.Name, Value = subcategoryItem.SpecialistId });
                }
            }

            return returnList;
        }

        public async Task<JsonResult> GetRoomByServiceId(string serviceId)
        {
            var returnList = await GetRoomSelectListItem(serviceId);

            return Json(returnList);
        }

        public async Task<List<SelectListItem>> GetRoomSelectListItem(string serviceId)
        {
            var returnList = new List<SelectListItem> { new SelectListItem { Text = "Please select room", Value = "" } };

            var resultList = await _roomService.GetRoomByServiceId(serviceId, AccessToken);

            if (resultList != null && resultList.Count() > 0)
            {
                foreach (var subcategoryItem in resultList.OrderBy(item => item.RoomName))
                {
                    returnList.Add(new SelectListItem() { Text = subcategoryItem.RoomName, Value = subcategoryItem.RoomId });
                }
            }

            return returnList;
        }
    }
}
