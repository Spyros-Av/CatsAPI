using CatsAPI.Services;

namespace CatsAPI.Jobs
{
    public class CatFetchJob
    {
        private readonly ICatService catService;
        private readonly IJobStatusService jobStatusService;

        public CatFetchJob(ICatService catService, IJobStatusService jobStatusService)
        {
            this.catService = catService;
            this.jobStatusService = jobStatusService;
        }

        public async Task ExecuteAsync(string jobId)
        {
            try
            {
                this.jobStatusService.UpdateJobStatus(jobId, "In Progress");
                var catsAdded = await catService.FetchAndSaveCatsAsync();
                this.jobStatusService.UpdateJobStatus(jobId, "Completed", catsAdded);
            }
            catch (Exception ex)
            {
                this.jobStatusService.UpdateJobStatus(jobId, "Failed", errorMessage: ex.Message);
            }
        }
    }
}
