using DiabetesRiskAssessment.Models;
using System.Threading.Tasks;

namespace DiabetesRiskAssessment.Services
{
    public interface IPatientService
    {
        Task<Patient> GetPatientByIdAsync(string patientId);
    }
}