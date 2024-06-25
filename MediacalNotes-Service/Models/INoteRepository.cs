using MedicalNotesService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalNotesService.Repositories
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetAllNotesAsync();
        Task<Note> GetNoteByIdAsync(string id);
        Task<Note> CreateNoteAsync(Note note);
        Task UpdateNoteAsync(string id, Note noteIn);
        Task DeleteNoteAsync(string id);
        Task<IEnumerable<Note>>GetNotesByPatientIdAsync(string patientId);

    }
}