﻿using Microsoft.AspNetCore.Mvc;
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

namespace NUS_ISS_14_Appointment_Buddy.Controllers
{
    public class AppointmentController : BaseController
    {
        private IAppointmentService _appointmentService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(IAppointmentService appointmentService, IOptions<AppSettings> appSettings, 
            ILogger<AppointmentController> logger) : base(logger)
        {
            _appointmentService = appointmentService;
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

            var apptRvm = new ResultViewModel<M.Appointment>(apptItems.Data, apptItems.PageIndex, apptItems.PageSize, apptItems.Count);

            return View(apptRvm);
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
    }
}
