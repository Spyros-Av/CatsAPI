namespace CatsAPI.Models.ApiResponses
{
    public class CatApiResponses
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Breed> Breeds { get; set; } = new List<Breed>();
    }

    public class Breed
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Temperament { get; set; }
    }
}
