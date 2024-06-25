using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using DiabetesRiskAssessment.Services;

namespace DiabetesRiskAssessment.Controllers

{

    [Route("api/[controller]")]

    [ApiController]

    public class RiskAssessmentController : ControllerBase

    {

        private readonly IRiskAssessmentService _riskAssessmentService;
 
        public RiskAssessmentController(IRiskAssessmentService riskAssessmentService)

        {

            _riskAssessmentService = riskAssessmentService;

        }
 
        [HttpGet("{patientId}")]

        public async Task<ActionResult<string>> AssessRisk(string patientId)

        {

            var riskLevel = await _riskAssessmentService.AssessRiskAsync(patientId);

            return Ok(riskLevel);

        }

    }

}



