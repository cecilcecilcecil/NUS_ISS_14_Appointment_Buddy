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

namespace NUS_ISS_14_Appointment_Buddy.Controllers
{
    public class PatientInfoController : BaseController
    {

        private IPatientInfoService _patientInfoService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly ILogger<PatientInfoController> _logger;
        public PatientInfoController(IPatientInfoService patientInfoService, IOptions<AppSettings> appSettings,
                    ILogger<PatientInfoController> logger) : base(logger)
        {
            _patientInfoService = patientInfoService;
            _appSettings = appSettings;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string nric, string patName)
        {
            ViewData["NRIC"] = nric;
            ViewData["PatientName"] = patName;

            var pageSize = _appSettings.Value.PageSize;
            var page = 1;

            M.PaginatedResults<M.PatientInfo> patItems = await _patientInfoService.GetPatientInfoBySearch(AccessToken, nric, patName, page, pageSize);

            var apptRvm = new ResultViewModel<M.PatientInfo>(patItems.Data, patItems.PageIndex, patItems.PageSize, patItems.Count);

            return View(apptRvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(string nric, string patName)
        {
            return Json(new
            {
                redirectUrl = Url.Action("Index", new { nric = nric, patName = patName })
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetPatientInfoPage(int page, string nric, string patName, string partialV)
        {
            var pageSize = _appSettings.Value.PageSize;

            var res = await _patientInfoService.GetPatientInfoBySearch(AccessToken, nric, patName, page, pageSize);

            object vm = new object();

            if (res != null)
            {
                vm = new ResultViewModel<M.PatientInfo>(res.Data, res.PageIndex, res.PageSize, res.Count);
            }

            return PartialView(partialV, vm);
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePatientInfo(string patID)
        {
            M.PatientInfo patItem;

            if (!string.IsNullOrEmpty(patID))
            {
                patItem = await _patientInfoService.GetPatientInfoById(patID, AccessToken);
                if (patItem.DeathDate != null)
                {
                    ViewBag.Info = "This patient has passed away.";
                }
            }
            else
            {
                patItem = new M.PatientInfo
                {
                    PatientId = Guid.NewGuid().ToString()            
                };
            }

            ViewBag.Titles = GetTitles();
            ViewBag.Genders = GetGenders();

            return View("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(bool confirm, string patId)
        {
            if (confirm)
            {
                await _patientInfoService.DeletePatientInfoById(patId, AccessToken);
            }
            return View("Index");
        }

        List<SelectListItem> GetTitles()
        {
            var returnList = new List<SelectListItem> { 
                new SelectListItem { Text = "Please select a title", Value = "" },
                new SelectListItem { Text = "Mr", Value = "Mr" },
                new SelectListItem { Text = "Mrs", Value = "Mrs" },
                new SelectListItem { Text = "Ms", Value = "Ms" },
                new SelectListItem { Text = "Prof", Value = "Prof" },
                new SelectListItem { Text = "Doctor", Value = "Doctor" },
                new SelectListItem { Text = "Lord", Value = "Lord" },
            };
            return returnList;
        }

        List<SelectListItem> GetGenders()
        {
            var returnList = new List<SelectListItem> {
                new SelectListItem { Text = "Please select a gender", Value = "" },
                new SelectListItem { Text = "Male", Value = "M" },
                new SelectListItem { Text = "Female", Value = "F" },
            };
            return returnList;
        }

    }
}
