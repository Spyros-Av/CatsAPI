using CatsAPI.Models.DTO;
using System.Collections.Concurrent;

namespace CatsAPI.Services
{
    public class JobStatusService : IJobStatusService
    {
        private static readonly ConcurrentDictionary<string, JobStatusDto> _jobStatuses = new();

        public void CreateJob(string jobId)
        {
            _jobStatuses[jobId] = new JobStatusDto
            {
                JobId = jobId,
                Status = "Pending",
                CreatedAt = DateTime.Now
            };
        }

        public void UpdateJobStatus(string jobId, string status, int? catsAdded = null, string? errorMessage = null)
        {
            if (_jobStatuses.TryGetValue(jobId, out var jobStatus))
            {
                jobStatus.Status = status;
                jobStatus.CatsAdded = catsAdded;
                jobStatus.ErrorMessage = errorMessage;

                if (status == "Completed" || status == "Failed")
                {
                    jobStatus.CompletedAt = DateTime.Now;
                }
            }
        }

        public JobStatusDto? GetJobStatus(string jobId)
        {
            _jobStatuses.TryGetValue(jobId, out var jobStatus);
            return jobStatus;
        }
    }
}
