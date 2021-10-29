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
using Microsoft.AspNetCore.Mvc.Rendering;
using AppointmentBuddy.Core.Common.Helper;

namespace NUS_ISS_14_Appointment_Buddy.Controllers
{
    public class SpecialistController : BaseController
    {

        private ISpecialistService _specialistService;
        private IServicesService _servicesService;

        private readonly IOptions<AppSettings> _appSettings;
        private readonly ILogger<SpecialistController> _logger;
        public SpecialistController(ISpecialistService specialistService, IServicesService servicesService, IOptions<AppSettings> appSettings,
                    ILogger<SpecialistController> logger) : base(logger)
        {
            _specialistService = specialistService;
            _servicesService = servicesService;
            _appSettings = appSettings;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string nric, string specName)
        {
            ViewData["NRIC"] = nric;
            ViewData["SpecialistName"] = specName;

            var pageSize = _appSettings.Value.PageSize;
            var page = 1;

            var allSvcs = await _servicesService.GetAllNonPageServices(AccessToken);

            M.PaginatedResults<M.Specialist> patItems = await _specialistService.GetSpecialistBySearch(AccessToken, nric, specName, page, pageSize);

            foreach (var pat in patItems.Data)
            {
                pat.ServicesName = allSvcs.FirstOrDefault(x => x.ServicesId == pat.ServicesId).Description;
            }

            var patRvm = new ResultViewModel<M.Specialist>(patItems.Data, patItems.PageIndex, patItems.PageSize, patItems.Count);

            return View(patRvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(string nric, string specName)
        {
            return Json(new
            {
                redirectUrl = Url.Action("Index", new { nric = nric, specName = specName })
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetSpecialistPage(int page, string nric, string specName, string partialV)
        {
            var pageSize = _appSettings.Value.PageSize;

            var res = await _specialistService.GetSpecialistBySearch(AccessToken, nric, specName, page, pageSize);

            object vm = new object();

            if (res != null)
            {
                vm = new ResultViewModel<M.Specialist>(res.Data, res.PageIndex, res.PageSize, res.Count);
            }

            return PartialView(partialV, vm);
        }

        [HttpGet]
        public IActionResult LinkToAddSpecialist()
        {
            return Json(new { redirectUrl = Url.Action("SpecialistDetail", "Specialist") });
        }

        [HttpGet]
        public async Task<IActionResult> SpecialistDetail(string specId)
        {
            Specialist specItem = new Specialist();

            if (!string.IsNullOrEmpty(specId))
            {
                var specI = await _specialistService.GetSpecialistById(specId, AccessToken);

                if (specI != null)
                {
                    specItem = new Specialist
                    {
                        SpecialistId = specI.SpecialistId,
                        Name = specI.Name,
                        ServicesId = specI.ServicesId,
                        Availability = specI.Availability,
                        ContactNo = specI.ContactNo,
                        EmailDomain = specI.EmailDomain,
                        EmailLocalPart = specI.EmailLocalPart,
                        NRIC = specI.Nric
                    };
                }
            }
            else
            {
                specItem = new Specialist
                {
                    SpecialistId = Guid.NewGuid().ToString()
                };
            }

            ViewBag.Services = await GetServices();

            return View(specItem);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSpecialistById(string specId)
        {
            string msgVal = "";
            var successValue = Constants.ErrorCodes.Failure;

            successValue = await _specialistService.DeleteSpecialistById(specId, AccessToken);

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
        public async Task<IActionResult> SaveSpecialist(Specialist specInfo)
        {
            string msgVal = "";

            int successValue = Constants.ErrorCodes.Failure;

            if (!Validator.IsNRICValid(specInfo.NRIC))
            {
                return Json(new { msgVal = msgVal, successVal = Constants.ErrorCodes.Failure });
            }

            M.Specialist specCore = new M.Specialist
            {
                SpecialistId = specInfo.SpecialistId,
                Name = specInfo.Name,
                Availability = specInfo.Availability,
                ContactNo = specInfo.ContactNo,
                EmailDomain = specInfo.EmailDomain,
                EmailLocalPart = specInfo.EmailLocalPart,
                Nric = specInfo.NRIC,
                ServicesId = specInfo.ServicesId,
                LastUpdatedBy = UserName,
                LastUpdatedById = UserId
            };

            successValue = await _specialistService.SaveSpecialist(specCore, AccessToken);

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
