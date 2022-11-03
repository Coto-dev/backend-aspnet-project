using System.Linq;
using BackendDev.Data.Models;

namespace BackendDev.Data.ViewModels
{
    public class MovieDetailsModel
    {
        public string Id { get; set; } 
        public string? Name { get; set; }
        public string? Poster { get; set; }
        public int Year { get; set; }
        public string? Country { get; set; }
        public List<GenreModel>? Genres { get; set; }
        public List<ReviewModel>? Reviews { get; set; }
        public int Time { get; set; }
        public string? Tagline { get; set; }
        public string? Description { get; set; }
        public string? Director { get; set; }
        public int? Budget { get; set; }
        public int? Fees { get; set; }
        public int AgeLimit { get; set; }

        public MovieDetailsModel(MovieModel model)
        {
            Id = model.Id.ToString();
            Name = model.Name;
            Poster = model.Poster;
            Year = model.Year;
            Country = model.Country;
            Genres = model.MovieGenres.Select(x=> new GenreModel(x)).ToList();
            Reviews = model.Reviews.Select(x => new ReviewModel(x)).ToList();
            Time = model.Time;
            Tagline = model.Tagline;
            Description = model.Description;
            Director = model.Director;
            Budget = model.Budget;
            Fees = model.Fees;
            AgeLimit = model.AgeLimit;
        }

        public MovieDetailsModel()
        {

        }
    }
}
