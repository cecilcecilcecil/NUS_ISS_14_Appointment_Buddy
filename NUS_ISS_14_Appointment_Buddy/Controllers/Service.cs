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

        public ViewResult AddService() => View();

        [HttpPost]
        public async Task<IActionResult> AddService(AppointmentBuddy.Core.Model.Services Service)
        {
            List<AppointmentBuddy.Core.Model.Services> ServiceList = new List<AppointmentBuddy.Core.Model.Services>();
            StringContent content = new StringContent(System.Text.Json.JsonSerializer.Serialize(Service), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync("http://localhost:63742/api/Services", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var apiResponseArray = "[" + apiResponse + "]";
                    ServiceList = JsonConvert.DeserializeObject<List<AppointmentBuddy.Core.Model.Services>>(apiResponseArray);
                    return View(ServiceList);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteService(int ServiceId)
        {
            using (var httpClient = new HttpClient())
            {
                List<AppointmentBuddy.Core.Model.Services> ServiceList = new List<AppointmentBuddy.Core.Model.Services>();

                using (var response = await httpClient.GetAsync("http://localhost:63742/api/Services/" + ServiceId))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var apiResponseArray = "[" + apiResponse + "]";
                        ServiceList = JsonConvert.DeserializeObject<List<AppointmentBuddy.Core.Model.Services>>(apiResponseArray);
                    }

                    foreach (var r in ServiceList)
                    {
                        r.IsDeleted = true;

                    }
                }

                var length = System.Text.Json.JsonSerializer.Serialize(ServiceList).Length;
                var lengthAft = length - 2;
                var test = System.Text.Json.JsonSerializer.Serialize(ServiceList).Substring(1, lengthAft);
                StringContent content = new StringContent(test, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("http://localhost:63742/api/Services/" + ServiceId, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                }

                using (var response = await httpClient.GetAsync("http://localhost:63742/api/Services"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ServiceList = JsonConvert.DeserializeObject<List<AppointmentBuddy.Core.Model.Services>>(apiResponse);
                }

                return View("Service", ServiceList);
            }
        }

        public async Task<IActionResult> UpdateService(int Id)
        {
            List<AppointmentBuddy.Core.Model.Services> ServiceList = new List<AppointmentBuddy.Core.Model.Services>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:63742/api/Services/" + Id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var apiResponseArray = "[" + apiResponse + "]";
                        ServiceList = JsonConvert.DeserializeObject<List<AppointmentBuddy.Core.Model.Services>>(apiResponseArray);
                    }
                }
            }

            return View(ServiceList);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateServiceSubmit(AppointmentBuddy.Core.Model.Services Service)
        {
            using (var httpClient = new HttpClient())
            {
                List<AppointmentBuddy.Core.Model.Services> ServiceList = new List<AppointmentBuddy.Core.Model.Services>();

                StringContent content = new StringContent(System.Text.Json.JsonSerializer.Serialize(Service), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("http://localhost:63742/api/Services/" + Service.Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                }

                using (var response = await httpClient.GetAsync("http://localhost:63742/api/Services"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ServiceList = JsonConvert.DeserializeObject<List<AppointmentBuddy.Core.Model.Services>>(apiResponse);
                }

                return View("Service", ServiceList);
            }
        }

    }
}
