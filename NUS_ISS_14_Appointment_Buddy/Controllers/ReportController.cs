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
using System.Drawing;
using OfficeOpenXml.Style;
using OfficeOpenXml;

namespace NUS_ISS_14_Appointment_Buddy.Controllers
{
    public class ReportController : BaseController
    {
        private IAppointmentService _appointmentService;
        private IServicesService _servicesService;
        private IIdentityService _identityService;
        private IRoomService _roomService;
        private ISpecialistService _specialistService;

        private readonly IOptions<AppSettings> _appSettings;
        private readonly ILogger<ReportController> _logger;
        private const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public ReportController(IAppointmentService appointmentService, IIdentityService identityService, IRoomService roomService, IServicesService servicesService,
            ISpecialistService specialistService,
            IOptions<AppSettings> appSettings,
            ILogger<ReportController> logger) : base(logger)
        {
            _appointmentService = appointmentService;
            _servicesService = servicesService;
            _roomService = roomService;
            _specialistService = specialistService;
            _identityService = identityService;
            _appSettings = appSettings;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DownloadReport(string dateFrom = "", string dateTo = "")
        {
            string borderColor = "#000000";
            string cellColor = "#f8f8f8";

            Color cellColorHex = ColorTranslator.FromHtml(cellColor);
            Color borderColorHex = ColorTranslator.FromHtml(borderColor);

            int rowCounter = 1;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("AppointmentReport");

                worksheet.Cells[rowCounter, 1].Value = "S/N";
                worksheet.Cells[rowCounter, 2].Value = "Name";
                worksheet.Cells[rowCounter, 3].Value = "Appointment Date";
                worksheet.Cells[rowCounter, 4].Value = "Appointment Start Time";
                worksheet.Cells[rowCounter, 5].Value = "Appointment End Time";
                worksheet.Cells[rowCounter, 6].Value = "Service";
                worksheet.Cells[rowCounter, 7].Value = "Specialist";
                worksheet.Cells[rowCounter, 8].Value = "Room";

                worksheet.Column(2).Width = 50;
                worksheet.Column(3).Width = 50;
                worksheet.Column(4).Width = 30;
                worksheet.Column(5).Width = 75;
                worksheet.Column(6).Width = 75;
                worksheet.Column(7).Width = 75;
                worksheet.Column(8).Width = 75;

                for (int i = 1; i < 9; i++)
                {
                    worksheet.Cells[rowCounter, i].Style.Font.Bold = true;
                    worksheet.Cells[rowCounter, i].Style.Border.BorderAround(ExcelBorderStyle.Thin, borderColorHex);
                }

                rowCounter++;

                IEnumerable<M.Appointment> apptItems = await _appointmentService.GetAllAppointmentsByDateRange(AccessToken, dateFrom, dateTo);

                foreach (var appt in apptItems)
                {
                    if (!string.IsNullOrEmpty(appt.ServiceId))
                    {
                        appt.ServiceName = (await _servicesService.GetServiceByServicesId(appt.ServiceId, AccessToken)).Description;
                        appt.RoomName = (await _roomService.GetRoomByRoomId(appt.RoomId, AccessToken)).RoomName;
                        appt.SpecialistName = (await _specialistService.GetSpecialistById(appt.SpecialistId, AccessToken)).Name;
                    }

                    for (int j = 1; j < 9; j++)
                    {
                        if (rowCounter % 2 == 0)
                        {
                            worksheet.Cells[rowCounter, j].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[rowCounter, j].Style.Fill.BackgroundColor.SetColor(cellColorHex);
                        }

                        worksheet.Cells[rowCounter, j].Style.Border.BorderAround(ExcelBorderStyle.Thin, borderColorHex);
                    }

                    bool timeValid = TimeSpan.TryParse(appt.AppointmentTime, out TimeSpan apptTime);
                    var endTime = apptTime.Add(new TimeSpan(0, 0, 1800));

                    worksheet.Cells[rowCounter, 1].Value = (rowCounter - 1).ToString();
                    worksheet.Cells[rowCounter, 2].Value = appt.Name;
                    worksheet.Cells[rowCounter, 3].Value = appt.AppointmentDate.GetValueOrDefault().ToString("dd/MM/yyyy");
                    worksheet.Cells[rowCounter, 4].Value = appt.AppointmentTime;
                    worksheet.Cells[rowCounter, 5].Value = endTime.ToString("%h") + ":" + endTime.ToString("mm");
                    worksheet.Cells[rowCounter, 6].Value = appt.ServiceName;
                    worksheet.Cells[rowCounter, 7].Value = appt.RoomName;
                    worksheet.Cells[rowCounter, 8].Value = appt.SpecialistName;

                    rowCounter++;
                }

                return new FileContentResult(package.GetAsByteArray(), XlsxContentType)
                {
                    FileDownloadName = "Appointment_Report_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx"
                };
            }
        }
    }
}
