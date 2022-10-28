using BackendDev.Data.Models;

namespace BackendDev.Data.ViewModels
{
    public class GenreModelDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }

        public GenreModelDTO(Guid id, string? name)
        {
            Id = id;
            Name = name;
        }
        public GenreModelDTO(GenreModel modelDb)
        {
            Id = modelDb.Id;
            Name = modelDb.Name;
        }
    }
}
