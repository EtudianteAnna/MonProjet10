using System.Collections.Generic;
using System.Threading.Tasks;
using PatientService.Models;
using PatientService.Repositories;

namespace PatientService.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;

        public PatientService(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            return await _repository.GetAllPatients();
        }

        public async Task<Patient> GetPatientById(string id)
        {
            return await _repository.GetPatientById(id);
        }

        public async Task<Patient> AddPatient(Patient patient)
        {
            return await _repository.AddPatient(patient);
        }

        public async Task<Patient> UpdatePatient(Patient patient)
        {
            return await _repository.UpdatePatient(patient);
        }

        public async Task DeletePatient(string id)
        {
            await _repository.DeletePatient(id);
        }
    }
}