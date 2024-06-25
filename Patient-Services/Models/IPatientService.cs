using System.Collections.Generic;
using System.Threading.Tasks;
using PatientService.Models;

namespace PatientService.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetAllPatients();
        Task<Patient> GetPatientById(string id);
        Task<Patient> AddPatient(Patient patient);
        Task<Patient> UpdatePatient(Patient patient);
        Task DeletePatient(string id);
    }
}