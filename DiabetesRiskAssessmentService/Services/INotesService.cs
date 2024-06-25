using System.Collections.Generic;
using System.Threading.Tasks;
using DiabetesRiskAssessment.Models;
 
namespace DiabetesRiskAssessment.Services
{
    public interface INotesService
    {
        Task<IEnumerable<Note>>GetNotesByPatientIdAsync (string patientId);
    }
}