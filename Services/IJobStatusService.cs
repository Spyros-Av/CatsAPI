using CatsAPI.Models.DTO;

namespace CatsAPI.Services
{
    public interface IJobStatusService
    {
            void CreateJob(string jobId);
            void UpdateJobStatus(string jobId, string status, int? catsAdded = null, string? errorMessage = null);
            JobStatusDto? GetJobStatus(string jobId);
    }
}
