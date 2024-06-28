using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiabetesRiskAssessment.Models;
using DiabetesRiskAssessment.Services;

namespace DiabetesRiskAssessment
{
    public class RiskAssessmentService : IRiskAssessmentService
    {
        private readonly IPatientService _patientService;
        private readonly INotesService _notesService;

        public RiskAssessmentService(IPatientService patientService, INotesService notesService)
        {
            _patientService = patientService;
            _notesService = notesService;
        }

        public async Task<string> AssessRiskAsync(string patientId)
        {
            var patient = await _patientService.GetPatientByIdAsync(patientId);
            var notes = await _notesService.GetNotesByPatientIdAsync(patientId);

            foreach (var note in notes)
            {
                Console.WriteLine($"Note: {note.Content}");
            }

            int triggerCount = notes.Count(note => DiabetesTriggerTerms.Terms.Any(term => note.Content.Contains(term, StringComparison.OrdinalIgnoreCase)));
            Console.WriteLine($"Trigger Count: {triggerCount}");

            if (triggerCount == 0)
            {
                return "None";
            }

            int age = DateTime.Now.Year - patient.BirthDate.Year;
            if (patient.BirthDate > DateTime.Now.AddYears(-age)) age--;

            if (triggerCount >= 8 || (triggerCount >= 7 && age > 30))
            {
                return "Early onset";
            }

            if ((triggerCount >= 6 && age > 30) ||
                (patient.Gender == "Male" && triggerCount >= 5 && age < 30) ||
                (patient.Gender == "Female" && triggerCount >= 7 && age < 30))
            {
                return "In Danger";
            }

            if (triggerCount >= 2 && triggerCount <= 5 && age > 30)
            {
                return "Borderline";
            }

            return "None";
        }
    }
}