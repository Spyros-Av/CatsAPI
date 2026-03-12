using System.ComponentModel.DataAnnotations;

namespace CatsAPI.Models.DTO
{
    public class GetCatsRequestDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Page must be at least 1")]
        public int Page { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "Page size must be between 1 and 100")]
        public int PageSize { get; set; } = 10;

        [StringLength(50, ErrorMessage = "Tag name cannot exceed 50 characters")]
        public string? Tag { get; set; }
    }
}
