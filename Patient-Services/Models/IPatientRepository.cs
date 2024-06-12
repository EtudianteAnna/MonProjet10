using System.Collections.Generic;
using System.Threading.Tasks;
using PatientService.Models;

namespace PatientService.Repositories
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatients();
        Task<Patient> GetPatientById(int id);
        Task<Patient> AddPatient(Patient patient);
        Task<Patient> UpdatePatient(Patient patient);
        Task DeletePatient(int id);
    }
}