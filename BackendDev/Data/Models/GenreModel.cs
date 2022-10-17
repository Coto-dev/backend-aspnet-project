namespace BackendDev.Data.Models
{
    public class GenreModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }   
    }
}
