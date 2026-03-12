namespace CatsAPI.Models.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public List<Cat> Cats { get; set; } = new List<Cat>();
    }
}
