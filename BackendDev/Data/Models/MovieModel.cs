using BackendDev.Data.ViewModels;

namespace BackendDev.Data.Models
{
    public class MovieModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? Poster { get; set; }
        public string? Description { get; set; }
        public int Year { get; set; }
        public string? Country { get; set; }
        public int Time { get; set; }
        public string? Tagline { get; set; }
        public string? Director { get; set; }
        public int? Budget { get; set; }
        public int? Fees { get; set; }
        public int AgeLimit { get; set; }
        public List<UserModel> UserMovies { get; set; } = new List<UserModel>();
        public List<GenreModelBd> MovieGenres { get; set; } = new List<GenreModelBd>();
        public List<ReviewModelBd> Reviews { get; set; } = new List<ReviewModelBd>();
        
        public MovieModel(MovieDetailsModel movieDetailsModelDTO)
        {
            Name = movieDetailsModelDTO.Name;
            Poster = movieDetailsModelDTO.Poster;   
            Description = movieDetailsModelDTO.Description;
            Year = movieDetailsModelDTO.Year;
            Country = movieDetailsModelDTO.Country;
            Time = movieDetailsModelDTO.Time;
            Tagline = movieDetailsModelDTO.Tagline;
            Director = movieDetailsModelDTO.Director;
            Budget = movieDetailsModelDTO.Budget;
            Fees = movieDetailsModelDTO.Fees;
            AgeLimit = movieDetailsModelDTO.AgeLimit;

        }
        public MovieModel()
        {

        }
    }
}
