using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ApiGateway.DTO;
using System.Text;
 
namespace ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiGatewayController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
 
        public ApiGatewayController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
 
        // GET: api/ApiGateway/patients
        [HttpGet("patients")]
        public async Task<IActionResult> GetPatients()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:3001/api/patients");
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
            var patients = await response.Content.ReadAsStringAsync();
            return Ok(JsonConvert.DeserializeObject(patients));
        }
 
        [HttpPost("patients")]
        public async Task<IActionResult> CreatePatient([FromBody] PatientDTO patient)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:3001/api/patients", content);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
            var createdPatient = await response.Content.ReadAsStringAsync();
            return CreatedAtAction(nameof(GetPatients), new { id = createdPatient }, createdPatient);
        }

        [HttpPut("patients/{id}")]
        public async Task<IActionResult> UpdatePatient(string id, [FromBody] PatientDTO patient)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"http://localhost:3001/api/patients/{id}", content);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }

            return NoContent();
        }

        // GET: api/ApiGateway/notes
        [HttpGet("notes")]
        public async Task<IActionResult> GetNotes()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:6200/api/notes");
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
            var notes = await response.Content.ReadAsStringAsync();
            return Ok(JsonConvert.DeserializeObject(notes));
        }
 
        // GET: api/ApiGateway/notes/patient/{patientId}
        [HttpGet("notes/patient/{patientId}")]
        public async Task<IActionResult> GetNotesByPatientId(string patientId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"http://localhost:6200/api/notes/patient/{patientId}");
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
            var notes = await response.Content.ReadAsStringAsync();
            return Ok(JsonConvert.DeserializeObject(notes));
        }
 
        [HttpPost("notes")]
        public async Task<IActionResult> CreateNote([FromBody] NoteDTO note)
        {
            note.CreatedDate = DateTime.UtcNow;
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(note), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:6200/api/notes", content);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
            var createdNote = await response.Content.ReadAsStringAsync();
            return CreatedAtAction(nameof(GetNotes), new { id = createdNote }, createdNote);
        }

        [HttpGet("riskassessment/{patientId}")]
        public async Task<IActionResult> AssessRisk(string patientId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"http://localhost:6300/api/riskassessment/{patientId}");

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response Content: " + responseContent);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, responseContent);
            }

            try
            {
                var riskLevel = responseContent.Trim('\"'); // Assurez-vous d'obtenir une chaîne propre sans guillemets
                return Ok(riskLevel);
            }
            catch (JsonException ex)
            {
                Console.WriteLine("JSON Parsing Error: " + ex.Message);
                return StatusCode(500, "Error parsing the response from the risk assessment API.");
            }
        }

    }
}