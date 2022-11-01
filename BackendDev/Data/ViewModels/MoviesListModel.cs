using BackendDev.Data.Models;

namespace BackendDev.Data.ViewModels
{
    public class MoviesListModel
    {
        public List<MovieElementModel>? movies { get; set; }
        
        public MoviesListModel(List<MovieModel>? movies)
        {
            this.movies = movies.Select(x => new MovieElementModel(x)).ToList(); ;
        }
        public MoviesListModel()
        {

        }
    }
}
