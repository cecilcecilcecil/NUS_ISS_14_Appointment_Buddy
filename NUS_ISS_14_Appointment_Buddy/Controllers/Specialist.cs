﻿using AppointmentBuddy.Core.Model;
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
        public async Task<IActionResult> IndexAsync(int id)
        {
            List<AppointmentBuddy.Core.Model.Specialist> SpecialistList = new List<AppointmentBuddy.Core.Model.Specialist>();

            if (id == 0)
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
                    using (var response = await httpClient.GetAsync("https://localhost:44341/api/Specialists/" + id))
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
        public async Task<IActionResult> DeleteSpecialist(int SpecialistId)
        {
            using (var httpClient = new HttpClient())
            {
                List<AppointmentBuddy.Core.Model.Specialist> SpecialistList = new List<AppointmentBuddy.Core.Model.Specialist>();

                using (var response = await httpClient.GetAsync("https://localhost:44341/api/Specialists/" + SpecialistId))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        SpecialistList = JsonConvert.DeserializeObject<List<AppointmentBuddy.Core.Model.Specialist>>(apiResponse);
                    }

                    foreach (var r in SpecialistList)
                    {
                        r.IsDeleted = true;
                    }
                }

                var length = System.Text.Json.JsonSerializer.Serialize(SpecialistList).Length;
                var lengthAft = length - 2;
                var test = System.Text.Json.JsonSerializer.Serialize(SpecialistList).Substring(1, lengthAft);
                StringContent content = new StringContent(test, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44341/api/Specialists/" + SpecialistId, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
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

        public async Task<IActionResult> UpdateSpecialist(int Id, CollectionDataModel model)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:63742/api/Services"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    model.Service = JsonConvert.DeserializeObject<List<AppointmentBuddy.Core.Model.Service>>(apiResponse);
                }

                using (var response = await httpClient.GetAsync("https://localhost:44341/api/Specialists/" + Id))
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
        public async Task<IActionResult> UpdateSpecialistDetail(int Id, CollectionDataModel model)
        {
            using (var httpClient = new HttpClient())
            {
                List<AppointmentBuddy.Core.Model.Specialist> SpecialistList = new List<AppointmentBuddy.Core.Model.Specialist>();

                model.modelSpec.Id = Id;
                
                var json = System.Text.Json.JsonSerializer.Serialize(model.modelSpec);

                StringContent content = new StringContent(System.Text.Json.JsonSerializer.Serialize(model.modelSpec), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44341/api/Specialists/" + Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var apiResponseArray = "[" + apiResponse + "]";
                    SpecialistList = JsonConvert.DeserializeObject<List<AppointmentBuddy.Core.Model.Specialist>>(apiResponseArray);
                }

                using (var response = await httpClient.GetAsync("https://localhost:44341/api/Specialists"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    SpecialistList = JsonConvert.DeserializeObject<List<AppointmentBuddy.Core.Model.Specialist>>(apiResponse);
                }

                return View("Specialist", SpecialistList);
            }
        }

    }
}
