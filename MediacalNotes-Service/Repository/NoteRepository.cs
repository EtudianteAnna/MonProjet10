using MedicalNotesService.Models;
using MedicalNotesService.Data;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
 
namespace MedicalNotesService.Repositories

{

    public class NoteRepository : INoteRepository

    {

        private readonly IMongoCollection<Note> _notes;
 
        public NoteRepository(MongoDbContext context)

        {

            _notes = context.Notes;

        }
 
        public async Task<IEnumerable<Note>> GetAllNotesAsync()

        {

            return await _notes.Find(note => true).ToListAsync();

        }
 
        public async Task<Note> GetNoteByIdAsync(string id)

        {

            return await _notes.Find(note => note.Id == id).FirstOrDefaultAsync();

        }
 
        public async Task<IEnumerable<Note>> GetNotesByPatientIdAsync(string patientId)

        {

            return await _notes.Find(note => note.PatientId == patientId).ToListAsync();

        }
 
        public async Task<Note> CreateNoteAsync(Note note)

        {

            await _notes.InsertOneAsync(note);

            return note;

        }
 
        public async Task UpdateNoteAsync(string id, Note noteIn)

        {

            await _notes.ReplaceOneAsync(note => note.Id == id, noteIn);

        }
 
        public async Task DeleteNoteAsync(string id)

        {

            await _notes.DeleteOneAsync(note => note.Id == id);

        }

    }

}
