using CatsAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobStatusService jobStatusService;
        public JobsController(IJobStatusService jobStatusService)
        {
            this.jobStatusService = jobStatusService;
        }

        [HttpGet("{jobId}")]
        public IActionResult GetJobStatus(string jobId)
        {
            if (string.IsNullOrWhiteSpace(jobId))
            {
                return BadRequest(new { message = "Job ID cannot be empty" });
            }

            if (!Guid.TryParse(jobId, out _))
            {
                return BadRequest(new { message = "Invalid job ID format. Must be a valid GUID." });
            }
            var jobStatus = jobStatusService.GetJobStatus(jobId);

            if (jobStatus == null)
            {
                return NotFound(new
                {
                    message = "Job not found",
                    jobId = jobId
                });
            }

            return Ok(jobStatus);
        }
    }
}
