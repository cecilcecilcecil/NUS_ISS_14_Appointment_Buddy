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

            var patRvm = new ResultViewModel<M.PatientInfo>(patItems.Data, patItems.PageIndex, patItems.PageSize, patItems.Count);

            return View(patRvm);
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
        public IActionResult LinkToAddPatient()
        {
            return Json(new { redirectUrl = Url.Action("PatientInfoDetail", "PatientInfo") });
        }

        [HttpGet]
        public async Task<IActionResult> PatientInfoDetail(string patID)
        {
            PatientInfo patItem = new PatientInfo();

            if (!string.IsNullOrEmpty(patID))
            {
                var patI = await _patientInfoService.GetPatientInfoById(patID, AccessToken);

                if (patI != null)
                {
                    patItem = new PatientInfo
                    {
                        PatientId = patI.PatientId,
                        PatientName = patI.PatientName,
                        BirthDate = patI.BirthDate,
                        ContactNumber = patI.ContactNumber,
                        DeathDate = patI.DeathDate,
                        Gender = patI.Gender,
                        NRIC = patI.NRIC,
                        Title = patI.Title
                    };
                }
            }
            else
            {
                patItem = new PatientInfo
                {
                    PatientId = Guid.NewGuid().ToString(),
                    DeathDate = null
                };
            }

            ViewBag.Titles = GetTitles();
            ViewBag.Genders = GetGenders();

            return View(patItem);
        }


        [HttpPost]
        public async Task<IActionResult> DeletePatientInfoById(bool confirm, string patId)
        {
            string msgVal = "";
            var successValue =  0;
            if (confirm)
            {
                successValue = await _patientInfoService.DeletePatientInfoById(patId, AccessToken);
            }
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
        public async Task<IActionResult> DeactivatePatientInfoById(bool confirm, string patId)
        {
            string msgVal = "";
            var successValue = 0;
            if (confirm)
            {
                successValue = await _patientInfoService.DeactivatePatientInfoById(patId, AccessToken);
            }
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
        public async Task<IActionResult> SavePatientInfoById(PatientInfo patInfo)
        {
            string msgVal = "";

            M.PatientInfo corePatInfo = new M.PatientInfo
            {
                PatientId = patInfo.PatientId,
                Title = patInfo.Title,
                NRIC = patInfo.NRIC,
                PatientName = patInfo.PatientName,
                Gender = patInfo.Gender,
                BirthDate = patInfo.BirthDate,
                ContactNumber = patInfo.ContactNumber,
                LastUpdatedBy = UserName,
                LastUpdatedById = UserId
            };

            var successValue = await _patientInfoService.SavePatientInfo(corePatInfo, AccessToken);

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

        public List<SelectListItem> GetTitles()
        {
            var returnList = new List<SelectListItem> { 
                new SelectListItem { Text = "Mr", Value = "Mr" },
                new SelectListItem { Text = "Mrs", Value = "Mrs" },
                new SelectListItem { Text = "Ms", Value = "Ms" },
                new SelectListItem { Text = "Prof", Value = "Prof" },
                new SelectListItem { Text = "Doctor", Value = "Doctor" },
                new SelectListItem { Text = "Lord", Value = "Lord" },
            };

            return returnList;
        }

        public List<SelectListItem> GetGenders()
        {
            var returnList = new List<SelectListItem> {
                new SelectListItem { Text = "Male", Value = "M" },
                new SelectListItem { Text = "Female", Value = "F" },
            };
            return returnList;
        }


        private static readonly int[] Multiples = { 2, 7, 6, 5, 4, 3, 2 };

        public static bool IsNRICValid(string nric)
        {
            if (string.IsNullOrEmpty(nric))
            {
                return false;
            }

            //	check length must be 9 digits
            if (nric.Length != 9)
            {
                return false;
            }

            int total = 0
                , count = 0
                , numericNric;
            char first = nric[0]
                , last = nric[nric.Length - 1];

            // first chat always S, T, F, G
            if (first != 'S' && first != 'T' && first != 'F' && first != 'G')
            {
                return false;
            }

            if (!int.TryParse(nric.Substring(1, nric.Length - 2), out numericNric))
            {
                return false;
            }

            while (numericNric != 0)
            {
                total += numericNric % 10 * Multiples[Multiples.Length - (1 + count++)];

                numericNric /= 10;
            }

            char[] outputs;
            if (first == 'S' || first == 'T')
            {
                outputs = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'Z', 'J' };
            }
            else
            {
                outputs = new char[] { 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'T', 'U', 'W', 'X' };
            }

            return last == outputs[total % 11];
        }

    }
}
