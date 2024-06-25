using MedicalNotesService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalNotesService.Services
{
    public interface INoteService
    {
        Task<IEnumerable<Note>> GetAllNotesAsync();
        Task<Note> GetNoteByIdAsync(string id);
        Task<IEnumerable<Note>> GetNotesByPatientIdAsync(string patientId); // Ajout de cette ligne
        Task<Note> CreateNoteAsync(Note note);
        Task UpdateNoteAsync(string id, Note noteIn);
        Task DeleteNoteAsync(string id);
    }
}