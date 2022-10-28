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
        public ReviewShortModel? Reviews { get; set; }
        public int Time { get; set; }
        public string? Tagline { get; set; }
        public string? Director { get; set; }
        public int? Budget { get; set; }
        public int? Fees { get; set; }
        public int AgeLimit { get; set; }

        /*public MovieDetailsModel(MovieModel model)
        {
            Id = model.Id.ToString();
            Name = model.Name;
            Poster = model.Poster;
            Year = model.Year;
            Country = model.Country;
            Genres = ;

        }*/
    }
}
