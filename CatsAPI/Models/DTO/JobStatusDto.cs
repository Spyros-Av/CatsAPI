namespace CatsAPI.Models.DTO
{
    public class JobStatusDto
    {
        public string JobId { get; set; } 
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int? CatsAdded { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
