using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MicroFrontEnd.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MicroFrontEnd.Controllers
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
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5106/api/ApiGateway/patients");
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
                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5106/api/ApiGateway/patients");
                request.Content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");

                var client = _clientFactory.CreateClient();
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(patient);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Patient patient)
        {
            if (patient == null)
            {
                return BadRequest("Patient data is null.");
            }

            var request = new HttpRequestMessage(HttpMethod.Put, $"http://localhost:5106/api/ApiGateway/patients/{patient.Id}");
            request.Content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }

            return StatusCode((int)response.StatusCode);
        }


        [HttpGet]
        public async Task<IActionResult> GetNotes(string patientId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5106/api/ApiGateway/notes/patient/{patientId}");
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var notes = JsonConvert.DeserializeObject<IEnumerable<Note>>(responseString);
                return Json(notes);
            }
            else
            {
                return Json(new List<Note>());
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetDiabetesRisk(string patientId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5106/api/ApiGateway/riskassessment/{patientId}");
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var riskLevel = await response.Content.ReadAsStringAsync();
                return Json(riskLevel);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNote([FromBody] Note note)
        {
            // Log the received note object
            Console.WriteLine("Received Note:");
            Console.WriteLine(JsonConvert.SerializeObject(note));

            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5106/api/ApiGateway/notes");
            request.Content = new StringContent(JsonConvert.SerializeObject(note), Encoding.UTF8, "application/json");

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }

            // Log the response status and content if the request fails
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response Status: " + response.StatusCode);
            Console.WriteLine("Response Content: " + responseContent);

            return StatusCode((int)response.StatusCode, responseContent);
        }
   
    }
}