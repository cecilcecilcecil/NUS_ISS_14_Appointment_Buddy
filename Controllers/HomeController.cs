using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using APIConsume.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System;
using Microsoft.AspNetCore.Http;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text.Json;

namespace APIConsume.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index(int id)
        {
            List<Specialist> SpecialistList = new List<Specialist>();

            if (id == 0)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://localhost:44341/api/Specialists"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        SpecialistList = JsonConvert.DeserializeObject<List<Specialist>>(apiResponse);
                    }
                }
                return View(SpecialistList);
            }
            else {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://localhost:44341/api/Specialists/" + id))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            SpecialistList = JsonConvert.DeserializeObject<List<Specialist>>(apiResponse);
                        }
                        else
                            ViewBag.StatusCode = response.StatusCode;
                    }
                }
                return View(SpecialistList);
            }
        }

        public ViewResult AddSpecialist() => View();

        [HttpPost]
        public async Task<IActionResult> AddSpecialist(Specialist Specialist)
        {
            using (var httpClient = new HttpClient())
            {
                Specialist receivedSpecialist = new Specialist();
                StringContent content = new StringContent(JsonConvert.SerializeObject(Specialist), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44341/api/Specialists", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedSpecialist = JsonConvert.DeserializeObject<Specialist>(apiResponse);
                    return View(receivedSpecialist);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSpecialistID(int id)
        {
            Specialist Specialist = new Specialist();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44341/api/Specialists/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Specialist = JsonConvert.DeserializeObject<Specialist>(apiResponse);
                }
            }
            return View(Specialist);
        }
        public ViewResult UpdateSpecialist() => View();

        public async Task<IActionResult> UpdateSpecialist(Specialist Specialist)
        {
            Specialist receivedSpecialist = new Specialist();
            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Specialist.Id.ToString()), "Id");
                content.Add(new StringContent(Specialist.Name), "Name");

                using (var response = await httpClient.PutAsync("https://localhost:44341/api/Specialists", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedSpecialist = JsonConvert.DeserializeObject<Specialist>(apiResponse);
                }
            }
            return View(receivedSpecialist);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSpecialist(int SpecialistId)
        {
            using (var httpClient = new HttpClient())
            {
                List<Specialist> SpecialistList = new List<Specialist>();

                using (var response = await httpClient.GetAsync("https://localhost:44341/api/Specialists/" + SpecialistId))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        SpecialistList = JsonConvert.DeserializeObject<List<Specialist>>(apiResponse);
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
            }

            return RedirectToAction("Index");
        }
    }
}