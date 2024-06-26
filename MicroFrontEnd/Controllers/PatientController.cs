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

        [HttpPost]
        public async Task<IActionResult> AddNote([FromBody] Note note)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5106/api/ApiGateway/notes");
            request.Content = new StringContent(JsonConvert.SerializeObject(note), Encoding.UTF8, "application/json");

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            return StatusCode((int)response.StatusCode);
        }
    }
}