using DiabetesRiskAssessment.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DiabetesRiskAssessment.Services
{
    public class NotesService : INotesService
    {
        private readonly HttpClient _httpClient;

        public NotesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Note>> GetNotesByPatientIdAsync(string patientId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:6200/api/notes/patient/{patientId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Note>>();
        }
    }
}