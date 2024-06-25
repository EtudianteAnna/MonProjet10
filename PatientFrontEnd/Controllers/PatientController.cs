using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PatientFrontEnd.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PatientFrontEnd.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public PatientsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:3001/api/patients");
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var patients = JsonConvert.DeserializeObject<IEnumerable<Patient>>(responseString);
                return View(patients);
            }
            else
            {
                return View(new List<Patient>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:3001/api/patients");
                request.Content = new StringContent(JsonConvert.SerializeObject(patient), System.Text.Encoding.UTF8, "application/json");

                var client = _clientFactory.CreateClient();
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(patient);
        }
    }
}

