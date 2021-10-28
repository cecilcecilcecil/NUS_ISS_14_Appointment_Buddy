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
    public class ServicesController : BaseController
    {
        private IServicesService _servicesService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly ILogger<ServicesController> _logger;

        public ServicesController(IServicesService servicesService, IOptions<AppSettings> appSettings,
            ILogger<ServicesController> logger) : base(logger)
        {
            _servicesService = servicesService;
            _appSettings = appSettings;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string desc)
        {
            ViewData["Description"] = desc;

            var pageSize = _appSettings.Value.PageSize;
            var page = 1;

            M.PaginatedResults<M.Services> apptItems = await _servicesService.GetAllServices(AccessToken, desc, page, pageSize);

            var apptRvm = new ResultViewModel<M.Services>(apptItems.Data, apptItems.PageIndex, apptItems.PageSize, apptItems.Count);

            return View(apptRvm);
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
        public IActionResult LinkToAddServices()
        {
            return Json(new { redirectUrl = Url.Action("CreateService", "Services") });
        }

        [HttpGet]
        public async Task<IActionResult> CreateService(string svcId = "")
        {
            Models.Services model;

            if (!string.IsNullOrEmpty(svcId))
            {
                var svc = await _servicesService.GetServiceByServicesId(svcId, AccessToken);

                model = new Models.Services
                {
                    ServicesId = svc.ServicesId,
                    Description = svc.Description
                };
            }
            else
            {
                model = new Models.Services 
                {
                    ServicesId = Guid.NewGuid().ToString(),
                };
            }

            return View("CreateService", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetServicesPage(int page, string desc, string partialV)
        {
            var pageSize = _appSettings.Value.PageSize;

            var res = await _servicesService.GetAllServices(AccessToken, desc, page, pageSize);

            object vm = new object();

            if (res != null)
            {
                vm = new ResultViewModel<M.Services>(res.Data, res.PageIndex, res.PageSize, res.Count);
            }

            return PartialView(partialV, vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveService(Models.Services svc)
        {
            string msgVal = "";

            M.Services coreSvc = new M.Services
            {
                ServicesId = svc.ServicesId,
                Description = svc.Description,
                LastUpdatedBy = UserName,
                LastUpdatedById = UserId
            };

            var successValue = await _servicesService.SaveService(coreSvc, AccessToken);

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
