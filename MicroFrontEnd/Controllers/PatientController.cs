using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MicroFrontEnd.Models;

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
                var client = _clientFactory.CreateClient();
                var jsonContent = JsonConvert.SerializeObject(patient);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("http://localhost:5106/api/ApiGateway/patients", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(patient);
        }
    }
}