using CatsAPI.Models.Entities;

namespace CatsAPI.Models.DTO
{
    public class TagDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
    }
}
