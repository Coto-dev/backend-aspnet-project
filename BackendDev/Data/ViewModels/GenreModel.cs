using BackendDev.Data.Models;

namespace BackendDev.Data.ViewModels
{
    public class GenreModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }

        public GenreModel(Guid id, string? name)
        {
            Id = id;
            Name = name;
        }
        public GenreModel(GenreModelBd modelDb)
        {
            Id = modelDb.Id;
            Name = modelDb.Name;
        }
       
    }
}
