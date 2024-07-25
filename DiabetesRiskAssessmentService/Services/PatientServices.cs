using DiabetesRiskAssessment.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiabetesRiskAssessment.Services
{
    public class PatientServices : IPatientService
    {
        private readonly HttpClient _httpClient;

        public PatientServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Patient> GetPatientByIdAsync(string patientId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:3001/api/patients/{patientId}");

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Patient>();
        }
    }
}