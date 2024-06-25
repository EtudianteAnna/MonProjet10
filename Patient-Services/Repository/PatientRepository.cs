using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatientService.Data;
using PatientService.Models;

namespace PatientService.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly PatientContext _context;

        public PatientRepository(PatientContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient> GetPatientById(string id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task<Patient> AddPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<Patient> UpdatePatient(Patient patient)  // Assure-toi que cette méthode est implémentée
        {
            _context.Entry(patient).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task DeletePatient(string id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }
    }
}