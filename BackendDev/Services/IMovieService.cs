using BackendDev.Data.Models;
using BackendDev.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BackendDev.Services
{
  public  interface IMovieService
    {
        public MovieDetailsModel GetMovieDetails(string Id);
        public Task<IActionResult> GetMoviePage(string Page);
    }
    public class MovieService : IMovieService
    {
        private readonly ContextDataBase _contextData;
        public MovieService(ContextDataBase contextData)
        {
            _contextData = contextData;
        }
       public MovieDetailsModel GetMovieDetails(string Id)
        {
            MovieDetailsModel modelDTO = null;
            foreach (MovieModel movieDetails in _contextData.MovieModels)
            {
                if (Id == movieDetails.Id.ToString())
                {
                    modelDTO = new MovieDetailsModel(movieDetails);
                  
                }
            }
            if (modelDTO != null)
            return modelDTO;
            else throw new ArgumentException("Фильм с таким Id не существует");
        }
        public Task<IActionResult> GetMoviePage(string Page) 
        {
            return null;
        }


    }
}
