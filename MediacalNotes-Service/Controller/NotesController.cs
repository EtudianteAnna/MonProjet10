using MedicalNotesService.Models;
using MedicalNotesService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalNotesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetAllNotes()
        {
            var notes = await _noteService.GetAllNotesAsync();
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNoteById(string id)
        {
            var note = await _noteService.GetNoteByIdAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }
       
            [HttpGet("patient/{patientId}")] 
            public async Task<ActionResult<IEnumerable<Note>>> GetNotesByPatientId(string patientId)
            { var notes = await _noteService.GetNotesByPatientIdAsync(patientId); if (notes == null || !notes.Any()) 
                { return NotFound(); }
                return Ok(notes); }

        [HttpPost]
        public async Task<ActionResult<Note>> CreateNote(Note note)
        {
            note.CreatedDate = DateTime.UtcNow;
            await _noteService.CreateNoteAsync(note);
            return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(string id, Note note)
        {
            var existingNote = await _noteService.GetNoteByIdAsync(id);

            if (existingNote == null)
            {
                return NotFound();
            }

            await _noteService.UpdateNoteAsync(id, note);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(string id)
        {
            var existingNote = await _noteService.GetNoteByIdAsync(id);

            if (existingNote == null)
            {
                return NotFound();
            }

            await _noteService.DeleteNoteAsync(id);
            return NoContent();
        }
    }
}
