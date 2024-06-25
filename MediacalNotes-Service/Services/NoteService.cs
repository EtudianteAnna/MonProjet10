using MedicalNotesService.Models;
using MedicalNotesService.Repositories;

namespace MedicalNotesService.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public Task<IEnumerable<Note>> GetAllNotesAsync()
        {
            return _noteRepository.GetAllNotesAsync();
        }

        public Task<Note> GetNoteByIdAsync(string id)
        {
            return _noteRepository.GetNoteByIdAsync(id);
        }

        public Task<Note> CreateNoteAsync(Note note)
        {
            return _noteRepository.CreateNoteAsync(note);
        }

        public Task UpdateNoteAsync(string id, Note noteIn)
        {
            return _noteRepository.UpdateNoteAsync(id, noteIn);
            }

        public Task<IEnumerable<Note>> GetNotesByPatientIdAsync(string patientId) { return _noteRepository.GetNotesByPatientIdAsync(patientId); }

        public Task DeleteNoteAsync(string id)
        {
            return _noteRepository.DeleteNoteAsync(id);
        }
    }
}