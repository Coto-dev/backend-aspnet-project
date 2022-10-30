using BackendDev.Data.Models;

namespace BackendDev.Data.ViewModels
{
    public class MovieElementModel
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Poster { get; set; }
        public int Year { get; set; }
        public string? Country { get; set; }
        public List<GenreModel>? Genres { get; set; }
        public List<ReviewShortModel>? Reviews { get; set; }


        public MovieElementModel(string id, string? name, string? poster, int year, string? country, List<GenreModel>? genres, List<ReviewShortModel>? reviews)
        {
            Id = id;
            Name = name;
            Poster = poster;
            Year = year;
            Country = country;
            Genres = genres;
            Reviews = reviews;
        }

        public MovieElementModel(MovieModel movieModelDb)
        {
            Id = movieModelDb.Id.ToString();
            Name = movieModelDb.Name;
            Poster = movieModelDb.Poster;
            Year = movieModelDb.Year;
            Country = movieModelDb.Country;
            Genres = movieModelDb.MovieGenres.Select(x => new GenreModel(x)).ToList();
            Reviews = movieModelDb.Reviews.Select(x => new ReviewShortModel(x)).ToList();

        }
    }
}
