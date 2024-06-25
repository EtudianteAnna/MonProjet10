namespace DiabetesRiskAssessment.Services
{
    public interface IRiskAssessmentService
    {
        Task<string> AssessRiskAsync(string patientId);
    }
}