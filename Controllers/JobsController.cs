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
