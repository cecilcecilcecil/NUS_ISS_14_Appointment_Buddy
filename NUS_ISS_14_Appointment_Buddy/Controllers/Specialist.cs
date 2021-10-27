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
    public class Specialist : Controller
    {
        public async Task<IActionResult> IndexAsync(String Name)
        {
            List<AppointmentBuddy.Core.Model.Specialist> SpecialistList = new List<AppointmentBuddy.Core.Model.Specialist>();

            if (Name == null)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://localhost:44341/api/Specialists"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        SpecialistList = JsonConvert.DeserializeObject<List<AppointmentBuddy.Core.Model.Specialist>>(apiResponse);
                    }
                }
                return View("Specialist",SpecialistList);
            }
            else
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://localhost:44341/api/Specialists/" + Name))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            SpecialistList = JsonConvert.DeserializeObject<List<AppointmentBuddy.Core.Model.Specialist>>(apiResponse);
                        }
                        else
                            ViewBag.StatusCode = response.StatusCode;
                    }
                }
                return View("Specialist",SpecialistList);
            }
        }

        public async Task<IActionResult> AddSpecialist()
        {
            CollectionDataModel model = new CollectionDataModel();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:63742/api/Services"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    model.Service = JsonConvert.DeserializeObject<List<AppointmentBuddy.Core.Model.Service>>(apiResponse);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddSpecialist(CollectionDataModel model)
        {
            using (var httpClient = new HttpClient())
            {
               // List<AppointmentBuddy.Core.Model.Specialist> SpecialistList = new List<AppointmentBuddy.Core.Model.Specialist>();

                StringContent content = new StringContent(System.Text.Json.JsonSerializer.Serialize(model.modelSpec), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44341/api/Specialists", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                   // SpecialistList = JsonConvert.DeserializeObject<List<AppointmentBuddy.Core.Model.Specialist>>(apiResponse);
                    //return View(model);
                }

                using (var response = await httpClient.GetAsync("http://localhost:63742/api/Services"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    model.Service = JsonConvert.DeserializeObject<List<AppointmentBuddy.Core.Model.Service>>(apiResponse);
                }

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSpecialist(string SpecialistName)
        {
            using (var httpClient = new HttpClient())
            {
                List<AppointmentBuddy.Core.Model.Specialist> SpecialistList = new List<AppointmentBuddy.Core.Model.Specialist>();
                int ID = 0;

                using (var response = await httpClient.GetAsync("https://localhost:44341/api/Specialists/" + SpecialistName))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        SpecialistList = JsonConvert.DeserializeObject<List<AppointmentBuddy.Core.Model.Specialist>>(apiResponse);
                    }

                    foreach (var r in SpecialistList)
                    {
                        r.IsDeleted = true;
                        ID = r.Id;
                    }
                }

                var length = System.Text.Json.JsonSerializer.Serialize(SpecialistList).Length;
                var lengthAft = length - 2;
                var test = System.Text.Json.JsonSerializer.Serialize(SpecialistList).Substring(1, lengthAft);
                StringContent content = new StringContent(test, Encoding.UTF8, "application/json");

                using (var responseD = await httpClient.PutAsync("https://localhost:44341/api/Specialists/" + ID, content))
                {
                    string apiResponse = await responseD.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                }

                using (var response = await httpClient.GetAsync("https://localhost:44341/api/Specialists"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    SpecialistList = JsonConvert.DeserializeObject<List<AppointmentBuddy.Core.Model.Specialist>>(apiResponse);
                }

                return View("Specialist", SpecialistList);
            }
        }

        public async Task<IActionResult> UpdateSpecialist(string name, CollectionDataModel model)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:63742/api/Services"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    model.Service = JsonConvert.DeserializeObject<List<AppointmentBuddy.Core.Model.Service>>(apiResponse);
                }

                using (var response = await httpClient.GetAsync("https://localhost:44341/api/Specialists/" + name))
                {
                    List<AppointmentBuddy.Core.Model.Specialist> SpecialistList = new List<AppointmentBuddy.Core.Model.Specialist>();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        model.Specialist = JsonConvert.DeserializeObject<List<AppointmentBuddy.Core.Model.Specialist>>(apiResponse);
                    }
                }
                
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSpecialist(CollectionDataModel model)
        {
            using (var httpClient = new HttpClient())
            {
                var json = System.Text.Json.JsonSerializer.Serialize(model.modelSpec);

                StringContent content = new StringContent(System.Text.Json.JsonSerializer.Serialize(model.modelSpec), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44341/api/Specialists/" + model.modelSpec.Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }

                using (var response = await httpClient.GetAsync("https://localhost:44341/api/Specialists/" + model.modelSpec.Name))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    model.Specialist = JsonConvert.DeserializeObject<List<AppointmentBuddy.Core.Model.Specialist>>(apiResponse);
                }

                using (var response = await httpClient.GetAsync("http://localhost:63742/api/Services"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    model.Service = JsonConvert.DeserializeObject<List<AppointmentBuddy.Core.Model.Service>>(apiResponse);
                }

                return View(model);
            }
        }

    }
}
