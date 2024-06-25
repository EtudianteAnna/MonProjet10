using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using PatientService.Models;
using PatientService.Services;

namespace PatientService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _service;

        public PatientsController(IPatientService service)
        {
            _service = service;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            var patients = await _service.GetAllPatients();
            return Ok(patients);
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(string id)
        {
            var patient = await _service.GetPatientById(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        // POST: api/Patients
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var newPatient = await _service.AddPatient(patient);

            return CreatedAtAction("GetPatient", new { id = newPatient.Id }, newPatient);
        }

        // PUT: api/Patients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(string id, Patient patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            await _service.UpdatePatient(patient);
            return NoContent();
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(string id)
        {
            await _service.DeletePatient(id);
            return NoContent();
        }
    }
}