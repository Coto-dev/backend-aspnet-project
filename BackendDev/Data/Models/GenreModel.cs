namespace BackendDev.Data.Models
{
    public class GenreModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public List<MovieModel> MovieGenres { get; set; } = new List<MovieModel>();
    }
}
