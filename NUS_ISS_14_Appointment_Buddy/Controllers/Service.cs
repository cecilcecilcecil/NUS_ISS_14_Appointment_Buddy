using AppointmentBuddy.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NUS_ISS_14_Appointment_Buddy.Controllers
{
    public class Service : Controller
    {
        public async Task<IActionResult> IndexAsync(int id)
        {
            List<AppointmentBuddy.Core.Model.Services> ServiceList = new List<AppointmentBuddy.Core.Model.Services>();

            if (id == 0)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:63742/api/Services"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ServiceList = JsonConvert.DeserializeObject<List<AppointmentBuddy.Core.Model.Services>>(apiResponse);
                    }
                }
                return View("Service", ServiceList);
            }
            else
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:63742/api/Services/" + id))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();

                            var apiResponseArray = "[" + apiResponse + "]";
                            ServiceList = JsonConvert.DeserializeObject<List<AppointmentBuddy.Core.Model.Services>>(apiResponseArray);
                        }
                    }
                }
                return View("Service", ServiceList);
            }
        }
    }
}
